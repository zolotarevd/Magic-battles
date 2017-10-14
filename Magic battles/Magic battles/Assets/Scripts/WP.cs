using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WP : MonoBehaviour {

	public Transform bulet;
	public float shrate = 0.2f;
	public Vector2 speedex = new Vector2 (2, 2);
	public float range = 0.5f;

	private float shcooldown;	

	// Use this for initialization
	void Start () {
		shcooldown = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (shcooldown > 0)
			shcooldown -= Time.deltaTime;
		
	}

	public void atack(bool isE){
		float inputX = Input.GetAxis("Horizontal");
		if (canAtack) {
			shcooldown = shrate;

			var shottransform = Instantiate (bulet) as Transform;

			shottransform.position = transform.position;

			Spel_kast shot = shottransform.gameObject.GetComponent <Spel_kast> ();

			if (shot != null) {
				shot.isEnemeyhurt = isE;
			}

			liner_move move = shottransform.gameObject.GetComponent <liner_move> ();
			speedex.x *= inputX;
			if (move != null) {
				move.speed = speedex;
			}
		}
	}
	public bool canAtack{
		get{
			return shcooldown <= 0f;
		}
	}
}
