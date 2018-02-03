using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float TtL = 10F;

	private float speed = 30F;
	private SpriteRenderer sprite;
	private Vector3 direction;


	new private Rigidbody2D rigidbody;
	public Vector2 Direction{ set { rigidbody.velocity = value * speed;} }


	private void Awake()
	{ 
		sprite = GetComponentInChildren<SpriteRenderer> ();
		rigidbody = GetComponent<Rigidbody2D> ();
	}

	private void Start()
	{
		if(TtL > 0)
			Destroy (gameObject, TtL);
	}

	void Update ()
	{
		transform.position = Vector3.MoveTowards (transform.position, transform.position + direction, speed * Time.deltaTime);
	}
}
