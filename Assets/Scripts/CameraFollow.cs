using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform player;
	public float speed = 1;

	CameraBounds2D bounds;
	Vector2 maxXPositions, maxYPositions;

    private void Awake()
    {
		bounds = FindObjectOfType<CameraBounds2D>();
		bounds.Initialize(GetComponent<Camera>());
		maxXPositions = bounds.maxXlimit;
		maxYPositions = bounds.maxYlimit;
	}

    void Update()
	{
		Vector3 currentPosition = transform.position;
		Vector3 targetPosition = new Vector3(Mathf.Clamp(player.position.x, maxXPositions.x, maxXPositions.y), Mathf.Clamp(player.position.y, maxYPositions.x, maxYPositions.y), currentPosition.z);
		transform.position = Vector3.Lerp(currentPosition, targetPosition, Time.deltaTime * speed);
	}
}
