using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LadderHole : Interactable
{
    private AudioSource mySource;

    private void Start()
    {
        mySource = GetComponent<AudioSource>();
    }

    public override void Interact()
    {
        base.Interact();
        if (interactedWith)
        {
            myTalk.NewTalk("hole");
        }
        else
        {
            myTalk.NewTalk("hole-first");
            interactedWith = true;
        }
    }

    public override void DoEvent(string key)
    {
        if(key == "sceneTransition")
        {
            myTalk.callback.RemoveAllListeners();
            myTalk.EndTalk();
            FadeManager.FadeOut(1);
            Invoke("TransitionScene", 1);
        }
    }

    public void TransitionScene()
    {
        //SceneManager.LoadScene("Dungeon-1");
    }
}
