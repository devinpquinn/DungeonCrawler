using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapSounds : MonoBehaviour
{
    public AudioSource snapSource;

    public List<AudioClip> snapSounds;

    private HelmetSounds myHelmet;

    private void Awake()
    {
        myHelmet = transform.GetComponentInChildren<HelmetSounds>();
    }

    public void PlaySnapSound()
    {
        int key = Random.Range(0, snapSounds.Count);
        snapSource.PlayOneShot(snapSounds[key]);
    }

    public void DelegateHelmetOpen()
    {
        myHelmet.PlayHelmetOpen();
    }
}
