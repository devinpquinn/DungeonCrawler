using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightDimmer : MonoBehaviour
{
    private Light2D l;
    public float intensity;

    private void Awake()
    {
        l = GetComponent<Light2D>();
        l.intensity = intensity;
    }
}
