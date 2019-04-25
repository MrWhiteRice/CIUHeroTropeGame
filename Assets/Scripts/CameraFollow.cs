using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	Transform player;
	public float speed = 5;

	private void Start()
	{
		player = GameObject.FindObjectOfType<Movement>().transform;
	}

	void Update()
    {
		if(player != null)
		{
			transform.position = Vector3.Lerp(transform.position, player.position, Time.deltaTime * speed);
		}
    }
}
