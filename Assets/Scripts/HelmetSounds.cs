using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetSounds : MonoBehaviour
{
    public AudioSource helmetSource;

    public AudioClip helmetOpen;
    public AudioClip helmetClose;

    public void PlayHelmetOpen()
    {
        helmetSource.PlayOneShot(helmetOpen);
    }

    public void PlayHelmetClose()
    {
        helmetSource.PlayOneShot(helmetClose);
    }
}
