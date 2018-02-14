using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Stuff : NetworkBehaviour
{	
	private GameObject bullet;
	private Transform bullettransform;
	private Vector2 bulletspeed;

	private float Shtimer = 0;
	private float Shdelay = 0.03F;

	private SpriteRenderer stuffsprite;
	private SpriteRenderer arcanasprite;

	private Mage_control MC;

	bool FlipX = false;

	/// ///////////////////////////////////////////////////////////////////////

	private void Awake()
	{
		MC = GetComponentInParent<Mage_control> ();
		//rigidbody = GetComponent<Rigidbody2D> ();
		SpriteRenderer[] SRs = GetComponentsInChildren<SpriteRenderer> ();
		stuffsprite = SRs[1];
		arcanasprite = SRs[0];
	}

	// Use this for initialization
	void Start ()
	{
		
	}

	//public override void OnStartLocalPlayer()
	//{
		
	//}

	/// ///////////////////////////////////////////////////////////////////////

	[Command]
	private void CmdCast()
	{
		Shtimer = 0;
		Vector3 position = transform.position; position.y += 0.5F; position.x += 0.5F * (FlipX ? 1.0F : -1.0F);
		var newbullet = (GameObject)Instantiate (bullet, position, bullettransform.rotation);
		Vector2 tmpspeed = bulletspeed;
		tmpspeed.x *= (FlipX ? -1 : 1);
		Vector2 addspeed;
		addspeed.x = Random.value;
		addspeed.y = Random.value;
		newbullet.GetComponent<Rigidbody2D> ().velocity = tmpspeed + addspeed;
		newbullet.GetComponentInChildren<SpriteRenderer> ().color = Random.ColorHSV (0, 1, 0, 1, 1, 1, 1, 1);
		NetworkServer.Spawn(newbullet);

		//arcanasprite.color = Random.ColorHSV(0, 1, 0, 1, 1, 1, 1, 1);
	}
	
	// Update is called once per frame
	private void Update()
	{
		FlipX = MC.FlipX;

		Shtimer += Time.deltaTime;
		//stuffsprite.flipX = FlipX;

		if (Input.GetButton ("Rotate"))
		{
			//startRotation ();
		}
		if (Input.GetButton ("Cast") && Shtimer >= Shdelay)
		{
			CmdCast ();
			Debug.Log("cast");
		}
	}
}
