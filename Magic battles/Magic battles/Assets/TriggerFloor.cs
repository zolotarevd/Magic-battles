using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFloor : MonoBehaviour {

	public bool isfloored = true;

	// Use this for initialization
	void Start () {
		isfloored = true;
	}

	void OnTriggerEnter2D(Collider2D other){
		isfloored = true;
	}

	void OnTriggerExit2D(Collider2D other){
		if ((GetComponent<Collider2D> ()).IsTouchingLayers ())
			Debug.Log("'5'");
		else isfloored = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
