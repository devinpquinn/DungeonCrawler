using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInAudio : MonoBehaviour
{
    [Range(0.1f, 1f)]
    public float targetVolume = 1f;

    private AudioSource src;

    private void Awake()
    {
        src = GetComponent<AudioSource>();
    }

    private void Start()
    {
        FadeInSound();
    }

    public void FadeInSound()
    {
        src.volume = 0f;
        StartCoroutine(DoFadeInSound());
    }

    IEnumerator DoFadeInSound()
    {
        while (src.volume < targetVolume)
        {
            src.volume += Time.deltaTime;
            yield return null;
        }
        src.volume = targetVolume;
        yield return null;
    }
}
