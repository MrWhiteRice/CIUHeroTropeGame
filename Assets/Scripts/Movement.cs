using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
	public float speed;
	public float jumpSpeed;

	public LayerMask groundLayer;

	public AudioClip jumpSound;
	public AudioClip deathSound;

	public GameObject dead;

	SpriteRenderer spr;
	Rigidbody2D rb;

	Vector2 direction;

	Vector2 lastPos;
	bool kill;
	bool jump;
	bool isJumping;
	bool movingForward = true;

	private void Start()
	{
		lastPos = transform.position;
		spr = GetComponentInChildren<SpriteRenderer>();
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
    {
		LastPos();

		Move();

		SpriteDirection();

		TryJump();
    }

	private void FixedUpdate()
	{
		if(jump)
		{
			if(IsGrounded())
			{
				Jump();
			}

			jump = false;
		}
		else
		{
			if(IsGrounded())
			{
				isJumping = false;
			}
		}

		if(kill)
		{
			Jump();

			kill = false;
		}

		rb.velocity = direction;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		switch(collision.tag)
		{
			case "Respawner":
				transform.position = lastPos;
				return;
			case "NextLevel":
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
				return;
		}

		if(collision.GetComponent<Enemy>())
		{
			//head jump
			if(direction.y < 0.1f && isJumping)
			{
				Destroy(collision.gameObject);

				GameObject jump = new GameObject("Death SFX");
				jump.AddComponent<AudioSource>().clip = deathSound;
				jump.GetComponent<AudioSource>().volume = 0.5f;
				jump.GetComponent<AudioSource>().Play();

				kill = true;
			}
			//touching normally
			else
			{
				int dir = collision.GetComponent<Enemy>().movingForward ? -1 : 1;

				GameObject corpse = Instantiate(dead, transform.position, Quaternion.identity);
				corpse.GetComponentInChildren<SpriteRenderer>().flipX = !collision.GetComponent<Enemy>().movingForward;
				corpse.GetComponent<Rigidbody2D>().velocity = new Vector2(dir * -10, 10);

				Destroy(this);
				Destroy(gameObject);
			}
		}
	}

	void LastPos()
	{
		if(Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundLayer))
		{
			lastPos = transform.position;
		}
	}

	void Jump()
	{
		direction.y = jumpSpeed;

		GameObject jump = new GameObject("Jump SFX");
		jump.AddComponent<AudioSource>().clip = jumpSound;
		jump.GetComponent<AudioSource>().volume = 0.5f;
		jump.GetComponent<AudioSource>().Play();

		isJumping = true;
	}

	bool IsGrounded()
	{
		if(Physics2D.OverlapCircle(transform.position, 0.15f, groundLayer))
		{
			return true;
		}

		return false;
	}

	bool CheckCollision()
	{
		Vector2 pos = transform.position;

		Vector2 direction = movingForward ? Vector2.right : Vector2.left;

		float length = GetComponent<CircleCollider2D>().radius;

		if(Physics2D.OverlapBox(pos + (direction * (length)) + (Vector2.up * 0.5f), new Vector2(0.05f, .5f), 0, groundLayer))
		{
			return true;
		}

		return false;
	}

	void Move()
	{
		direction = rb.velocity;
		float x = Input.GetAxisRaw("Horizontal");

		direction.x = 0;

		if(x != 0)
		{
			movingForward = x > 0 ? true : false;
		}

		if(!CheckCollision())
		{
			direction.x = x * speed;
		}
	}

	void SpriteDirection()
	{
		spr.flipX = !movingForward;
	}

	void TryJump()
	{
		if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
		{
			jump = true;
		}
	}
}