using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    public Transform background;
    private float parallaxScale;
    public float smoothing = 1f;

    public Transform cam;
    private Vector3 previousCamPos;

    private void Start()
    {
        previousCamPos = cam.position;

        parallaxScale = background.position.z * -1f;
    }

    private void FixedUpdate()
    {
        float parallaxX = (previousCamPos.x - cam.position.x) * parallaxScale;

        float backgroundTargetPosX = background.position.x + parallaxX;


        float parallaxY = (previousCamPos.y - cam.position.y) * parallaxScale;

        float backgroundTargetPosY = background.position.y + parallaxY;


        Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgroundTargetPosY, background.position.z);

        background.position = Vector3.Lerp(background.position, backgroundTargetPos, smoothing * Time.deltaTime);

        previousCamPos = cam.position;
    }
}
