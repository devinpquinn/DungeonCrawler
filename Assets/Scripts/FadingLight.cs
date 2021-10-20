using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FadingLight : MonoBehaviour
{
    private Light2D myLight;
    float startingIntensity;
    public float fadeDuration = 1f;
    float elapsed = 0f;

    private void Awake()
    {
        myLight = GetComponent<Light2D>();
        startingIntensity = myLight.intensity;
    }

    private void Update()
    {
        myLight.intensity = Mathf.Lerp(startingIntensity, 0, elapsed / fadeDuration);
        elapsed += Time.deltaTime;
        if (elapsed >= fadeDuration)
        {
            Destroy(this.gameObject);
        }
    }
}
