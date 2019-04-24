using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	Rigidbody2D rb;

	bool movingForward = true;
	float speed = 1;

	public GameObject point;
	public LayerMask mask;

	void Start()
    {
		rb = GetComponent<Rigidbody2D>();
    }

	private void Update()
	{
		Vector2 pos = transform.position;

		point.transform.position = pos + new Vector2(0.5f * CheckDir(), -0.15f);

		if(!Physics2D.OverlapCircle(point.transform.position, 0.1f, mask))
		{
			movingForward = !movingForward;
			print("flip!");
		}
	}

	void FixedUpdate()
	{
		Vector2 velo = rb.velocity;
		velo.x = CheckDir() * speed;
		print(velo.x);

		rb.velocity = velo;
	}

	int CheckDir()
	{
		return (movingForward) ? 1 : -1;
	}
}