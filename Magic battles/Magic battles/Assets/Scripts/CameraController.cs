using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraController : MonoBehaviour
{


	[SerializeField]
	private float speed = 2F;
	[SerializeField]
	private Transform target;

	private void Update()
	{
		Vector3 position = target.position; position.z = transform.position.z; position.y += 5F;
		transform.position = Vector3.Lerp (transform.position, position, speed * Time.deltaTime);
	}
}
