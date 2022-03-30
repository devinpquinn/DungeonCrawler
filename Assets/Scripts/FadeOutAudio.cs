using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutAudio : MonoBehaviour
{
    private AudioSource src;
    private void Awake()
    {
        src = GetComponent<AudioSource>();
    }

    private void Start()
    {
        FadeOutSound();
    }

    public void FadeOutSound()
    {
        StartCoroutine(DoFadeOutSound());
    }

    IEnumerator DoFadeOutSound()
    {
        while (src.volume > 0)
        {
            src.volume -= Time.deltaTime;
            yield return null;
        }
        src.volume = 0f;
        yield return null;
    }
}
