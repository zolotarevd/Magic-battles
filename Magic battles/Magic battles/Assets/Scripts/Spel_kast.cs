using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spel_kast : MonoBehaviour {

	public int damage = 1;
	public bool isEnemeyhurt = false;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, 20);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
