using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSounds : MonoBehaviour
{
    public AudioSource footstepSource;

    public List<AudioClip> stoneSounds;
    public List<AudioClip> earthSounds;
    public List<AudioClip> grassSounds;

    public void PlayFootstepSound(string stepMaterial)
    {
        switch (stepMaterial)
        {
            case "Stone":
                footstepSource.PlayOneShot(GetStoneSound());
                break;
            case "Earth":
                footstepSource.PlayOneShot(GetEarthSound());
                break;
            case "Grass":
                footstepSource.PlayOneShot(GetGrassSound());
                break;
        }
    }

    public AudioClip GetStoneSound()
    {
        int key = Random.Range(0, stoneSounds.Count);
        return stoneSounds[key];
    }

    public AudioClip GetEarthSound()
    {
        int key = Random.Range(0, earthSounds.Count);
        return stoneSounds[key];
    }

    public AudioClip GetGrassSound()
    {
        int key = Random.Range(0, grassSounds.Count);
        return stoneSounds[key];
    }
}
