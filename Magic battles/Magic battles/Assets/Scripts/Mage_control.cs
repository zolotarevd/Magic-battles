using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Mage_control : Unit
{

	[SerializeField]
	private float speed = 15.0F;
	[SerializeField]
	private float Mspeed = 30.0F;
	//[SerializeField]
	//private int lives = 5;
	[SerializeField]
	private float Jforce = 15.0F;
	[SerializeField]
	private float Jdelay = 1F;
	[SerializeField]
	private float Shdelay = 2F;

	[SerializeField]
	public GameObject bullet;
	[SerializeField]
	public Transform bullettransform;
	[SerializeField]
	public float bulletspeed;

	private float timer = 0;
	private float Shtimer = 0;

	new private Rigidbody2D rigidbody;
	private Animator animator;

	private SpriteRenderer sprite;
	private SpriteRenderer patern;

	[SyncVar]
	private bool isGrounded = false;
	[SyncVar]
	public bool FlipX = false;

	public GameObject stuff;

	public GameObject a;

	private MageState state
	{
		get{ return (MageState)animator.GetInteger("state"); }
		set{ animator.SetInteger("state", (int)value); }
	}

	private void Awake()
	{

		stuff.transform.Rotate (0, 0, 90);

		rigidbody = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		SpriteRenderer[] SRs = GetComponentsInChildren<SpriteRenderer> ();
		sprite = SRs[1];
		patern = SRs[0];
	}

	public override void OnStartLocalPlayer()
	{
		patern.color = Color.red;
	}

	// ///////////////////////////////////////////////////////////////////////////

	[Command]
	private void CmdTurn( float input)
	{
		Vector3 direction = transform.right * input;

		FlipX = direction.x > 0F;
		sprite.flipX = FlipX;
		patern.flipX = FlipX;
	}

	private void Run( float input)
	{	
		Vector2 movement = rigidbody.velocity;
		Vector3 direction = transform.right * input;

		movement.x = speed * input;
		rigidbody.velocity = movement;


		if(isGrounded) state = MageState.Run;
	}

	[Command]
	private void CmdCast()
	{
		Shtimer = 0;
		Vector3 position = stuff.transform.position; position.y += 0.5F; position.x += 0.5F * (FlipX ? 1.0F : -1.0F);
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

	/// <summary>
	/// Jump this instance.
	/// </summary>
	private void Jump()
	{
		rigidbody.AddForce (transform.up * Jforce, ForceMode2D.Impulse);
		timer = 0;
	}
		
	private void CheckGround()
	{
		isGrounded = (a.GetComponent<TriggerFloor> ()).isfloored;
	}

	// ///////////////////////////////////////////////////////////////////////////

	private void Update()
	{
		Quaternion Sotation = Quaternion.Euler (0, 0, 90);
		Debug.Log(Sotation.z);
		CheckGround ();

		sprite.flipX = FlipX;
		patern.flipX = FlipX;

		if (FlipX && stuff.transform.rotation.z > -Sotation.z)
			stuff.transform.Rotate (Vector3.forward, -10 * (Sotation.z + stuff.transform.rotation.z));
		
		if (!FlipX && stuff.transform.rotation.z < Sotation.z)
			stuff.transform.Rotate (Vector3.forward, 10 * (Sotation.z - stuff.transform.rotation.z));
		
		if(isGrounded)
			state = MageState.Idle;
		else 
			state = MageState.Jump;

		if (isLocalPlayer)
		{
			if (Input.GetButton ("Horizontal") && isGrounded)
			{
				float input = Input.GetAxis("Horizontal");
				Run (input);
				CmdTurn (input);
			}
			if (Input.GetButton ("Cast") && Shtimer >= Shdelay)
			{
				CmdCast ();
				Debug.Log("cast");
			}
			if (Input.GetButton ("Jump") && isGrounded && timer >= Jdelay && rigidbody.velocity.magnitude <= Mspeed)
				Jump ();
		}
		timer += Time.deltaTime;

	}

	void FixedUpdate()
	{
		Shtimer += Time.deltaTime;
		CheckGround ();
	}

	// ///////////////////////////////////////////////////////////////////////////
}

public enum MageState
{
	Idle,
	Run,
	Jump,
	Cast
}