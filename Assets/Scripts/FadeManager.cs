using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FadeManager : MonoBehaviour
{
    private static FadeManager instance;
    public static FadeManager Instance { get { return instance; } }

    private Volume fadeVolume;
    private float timeElapsed = 0;

    private void Awake()
    {
        //singleton
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        fadeVolume = GetComponent<Volume>();
    }

    public static void FadeIn(float duration)
    {
        instance.StartCoroutine(instance.DoFadeIn(duration));
    }

    public static void FadeOut(float duration)
    {
        instance.StartCoroutine(instance.DoFadeOut(duration));
    }

    public static void CrossFade(float durationOut, float durationIn)
    {
        instance.StartCoroutine(instance.DoCrossFade(durationOut, durationIn));
    }

    IEnumerator DoFadeIn(float duration)
    {
        fadeVolume.weight = 1;
        timeElapsed = 0;

        while(timeElapsed < duration)
        {
            fadeVolume.weight = Mathf.Lerp(1, 0, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        fadeVolume.weight = 0;
        yield return null;
    }

    IEnumerator DoFadeOut(float duration)
    {
        fadeVolume.weight = 1;
        timeElapsed = 0;

        while (timeElapsed < duration)
        {
            fadeVolume.weight = Mathf.Lerp(0, 1, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        fadeVolume.weight = 1;
        yield return null;
    }

    IEnumerator DoCrossFade(float durationOut, float durationIn)
    {
        fadeVolume.weight = 1;
        timeElapsed = 0;

        while (timeElapsed < durationOut)
        {
            fadeVolume.weight = Mathf.Lerp(0, 1, timeElapsed / durationOut);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        fadeVolume.weight = 1;
        timeElapsed = 0;

        while (timeElapsed < durationIn)
        {
            fadeVolume.weight = Mathf.Lerp(1, 0, timeElapsed / durationIn);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        fadeVolume.weight = 0;
        yield return null;
    }
}
