using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	public int hp = 1;
	public bool isE = true;

	public void Damage(int DC){
		
		hp -= DC;
		if (hp <= 0)
			Destroy (gameObject);
	}

	void OnTriggerEnter2D (Collider2D otherCollider){
		
		Spel_kast shot = otherCollider.gameObject.GetComponent<Spel_kast> ();
		if (shot != null) {
			Damage (shot.damage);
			Destroy (shot.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
}
