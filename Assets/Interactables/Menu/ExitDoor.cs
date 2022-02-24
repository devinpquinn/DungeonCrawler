using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : Interactable
{
    public MenuScript ms;

    public override void Interact()
    {
        StartCoroutine(DoQuit());
    }

    IEnumerator DoQuit()
    {
        FadeManager.FadeOut(1f);
        ms.FadeOutAudio();
        yield return new WaitForSeconds(1f);
        Application.Quit();
    }
}
