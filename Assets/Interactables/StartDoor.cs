using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartDoor : Interactable
{
    public string firstScene;

    public override void Interact()
    {
        StartCoroutine(DoStart());
    }

    IEnumerator DoStart()
    {
        FadeManager.FadeOut(1f);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(firstScene);
    }
}
