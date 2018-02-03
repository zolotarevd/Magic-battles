using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Unit : NetworkBehaviour
{

	public virtual void ReceiveDamage()
	{
		Die ();
	}
	public virtual void Die()
	{
		Destroy (gameObject);
	}
}
