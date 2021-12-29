using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform player;
	public Vector3 offset = new Vector3(0, 1, -10);
	public float speed = 1;

	void Update()
	{
		Vector3 currentPosition = transform.position;
		Vector3 targetPosition = player.position + offset;
		transform.position = Vector3.Lerp(currentPosition, targetPosition, Time.deltaTime * speed);
	}
}
