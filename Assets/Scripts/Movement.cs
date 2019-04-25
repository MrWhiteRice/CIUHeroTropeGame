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
		if(jump)
		{
			if(IsGrounded())
			{
				rb.AddForce((Vector2.up * rb.velocity.y) + (Vector2.up * jumpSpeed));
			}

			jump = false;
		}

		rb.velocity = direction;
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

	void Jump()
	{
		if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
		{
			jump = true;
		}
	}
}