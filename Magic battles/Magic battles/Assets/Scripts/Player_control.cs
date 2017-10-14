using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_control : MonoBehaviour {

	public float speed = 1;
	public float jump = 5;

	private Vector2 movement;
	private Rigidbody2D rigi;

	void Start () {
		rigi = GetComponent<Rigidbody2D>();
	}

	void Update () {
		float inputX = Input.GetAxis("Horizontal");

		movement = new Vector2(
			speed * inputX,
			rigi.velocity.y);
	}

	void FixedUpdate()
	{
		rigi.velocity = movement;
	}
}
