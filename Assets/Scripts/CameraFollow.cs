using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform player;
	public Vector3 offset = new Vector3(0, 1, -10);
	public float speed = 1;

	[SerializeField] CameraBounds2D bounds;
	Vector2 maxXPositions, maxYPositions;

    private void Awake()
    {
		bounds.Initialize(GetComponent<Camera>());
		maxXPositions = bounds.maxXlimit;
		maxYPositions = bounds.maxYlimit;
	}

    void Update()
	{
		Vector3 currentPosition = transform.position;
		Vector3 targetPosition = new Vector3(Mathf.Clamp(player.position.x, maxXPositions.x, maxXPositions.y), Mathf.Clamp(player.position.y, maxYPositions.x, maxYPositions.y), currentPosition.z) + offset;
		transform.position = Vector3.Lerp(currentPosition, targetPosition, Time.deltaTime * speed);
	}
}
