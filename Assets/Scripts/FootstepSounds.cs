using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSounds : MonoBehaviour
{
    public AudioSource footstepSource;

    public List<AudioClip> stoneSounds;
    public List<AudioClip> earthSounds;
    public List<AudioClip> grassSounds;
    public List<AudioClip> woodSounds;

    public AudioClip cloakFall;

    private int lastIndex;

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
            case "Wood":
                footstepSource.PlayOneShot(GetWoodSound());
                break;
        }
    }

    public AudioClip GetStoneSound()
    {
        int key = Random.Range(0, stoneSounds.Count);
        while(key == lastIndex)
        {
            key = Random.Range(0, stoneSounds.Count);
        }
        lastIndex = key;
        return stoneSounds[key];
    }

    public AudioClip GetEarthSound()
    {
        int key = Random.Range(0, earthSounds.Count);
        while (key == lastIndex)
        {
            key = Random.Range(0, earthSounds.Count);
        }
        lastIndex = key;
        return earthSounds[key];
    }

    public AudioClip GetGrassSound()
    {
        int key = Random.Range(0, grassSounds.Count);
        while (key == lastIndex)
        {
            key = Random.Range(0, grassSounds.Count);
        }
        lastIndex = key;
        return grassSounds[key];
    }

    public AudioClip GetWoodSound()
    {
        int key = Random.Range(0, woodSounds.Count);
        while (key == lastIndex)
        {
            key = Random.Range(0, woodSounds.Count);
        }
        lastIndex = key;
        return woodSounds[key];
    }

    public void PlayCloakFall()
    {
        footstepSource.PlayOneShot(cloakFall);
    }
}
