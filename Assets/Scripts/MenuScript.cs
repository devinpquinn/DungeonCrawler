using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public AudioSource ambientAudio;

    // Start is called before the first frame update
    void Start()
    {
        PlayerController.Instance.myState = PlayerController.playerState.Immobilized;
        FadeManager.FadeIn(1f);
    }

    public void FadeOutAudio()
    {
        StartCoroutine(DoFadeOutAudio());
    }

    IEnumerator DoFadeOutAudio()
    {
        while(ambientAudio.volume > 0)
        {
            ambientAudio.volume -= Time.deltaTime;
            yield return null;
        }
        ambientAudio.volume = 0;
        yield return null;
    }
}
