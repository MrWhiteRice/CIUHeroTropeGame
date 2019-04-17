using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	[SerializeField]
	float speed;

	[SerializeField]
	float jumpSpeed;

	[SerializeField]
	LayerMask groundLayer;

	SpriteRenderer spr;
	Rigidbody2D rb;

	Vector2 direction;

	bool jump;
	bool movingForward = true;

	private void Start()
	{
		spr = GetComponentInChildren<SpriteRenderer>();
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
    {
		Move();

		SpriteDirection();

		Jump();
    }

	private void FixedUpdate()
	{
		if(jump && IsGrounded())
		{
			rb.AddForce((Vector2.up * rb.velocity.y) + (Vector2.up * jumpSpeed));
			jump = false;
		}
		
		rb.velocity = direction;
	}

	bool IsGrounded()
	{
		if(Physics2D.OverlapCircle(transform.position, 0.15f, groundLayer))
		{
			print("collider");
			return true;
		}

		return false;
	}

	bool CheckCollision(bool forward)
	{
		Vector2 pos = transform.position;

		Vector2 direction;
		if(forward)
		{
			direction = Vector2.right;
		}
		else
		{
			direction = Vector2.left;
		}

		if(Physics2D.OverlapBox(pos + (direction * (0.5f)) + (Vector2.up * 0.5f), new Vector2(0.05f, .75f), 0, groundLayer))
		{
			print("collision");
			return true;
		}

		return false;
	}

	void Move()
	{
		float x = Input.GetAxisRaw("Horizontal");

		direction.x = 0;

		if(x > 0)
		{
			movingForward = true;

			if(!CheckCollision(true))
			{
				direction.x = x * speed;
			}
		}
		else if(x < 0)
		{
			movingForward = false;

			if(!CheckCollision(false))
			{
				direction.x = x * speed;
			}
		}
		
		direction.y = rb.velocity.y;
	}

	void SpriteDirection()
	{
		spr.flipX = !movingForward;
	}

	void Jump()
	{
		if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
		{
			jump = true;
		}
	}
}