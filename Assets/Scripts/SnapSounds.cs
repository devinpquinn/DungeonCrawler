using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapSounds : MonoBehaviour
{
    public AudioSource snapSource;

    public List<AudioClip> snapSounds;

    public void PlaySnapSound()
    {
        int key = Random.Range(0, snapSounds.Count);
        snapSource.PlayOneShot(snapSounds[key]);
    }
}
