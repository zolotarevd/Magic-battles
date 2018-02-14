using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Unit : NetworkBehaviour
{
	[SerializeField]
	private int HP = 100;

	public virtual void Die()
	{
		Destroy (gameObject);
	}

	public virtual void ReceiveDamage()
	{
		HP--;
		if(HP <= 0)
			Die ();
	}

	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		Bullet shoot = otherCollider.gameObject.GetComponent<Bullet> ();
		if (shoot != null)
			ReceiveDamage();
	}
}
