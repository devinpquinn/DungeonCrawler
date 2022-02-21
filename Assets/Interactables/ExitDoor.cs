using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : Interactable
{
    public override void Interact()
    {
        StartCoroutine(DoQuit());
    }

    IEnumerator DoQuit()
    {
        FadeManager.FadeOut(1f);
        yield return new WaitForSeconds(1f);
        Application.Quit();
    }
}
