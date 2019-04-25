using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimDetection : MonoBehaviour
{
	Animator anim;
	Rigidbody2D rb;

    void Start()
    {
		anim = GetComponent<Animator>();
		rb = GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
		float hor = Mathf.Abs(rb.velocity.x);
		anim.SetFloat("horizontalSpeed", hor);
    }
}
