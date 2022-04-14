using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAudioPlayer : MonoBehaviour
{
    private AudioSource src;
    public AudioClip clip;

    private void Awake()
    {
        src = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        src.PlayOneShot(clip);
    }
}
