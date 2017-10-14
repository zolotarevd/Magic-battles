using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class liner_move : MonoBehaviour {

	public Vector2 speed = new Vector2 (2, 2);


	private Vector2 movement;
	private Rigidbody2D rigi;

	void Start () {
		rigi = GetComponent<Rigidbody2D>();
		movement = new Vector2(
			speed.x,
			speed.y);
		rigi.velocity = movement;
	}
}
