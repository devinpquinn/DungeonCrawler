using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Interactable
{
    public string DestinationScene;
    public string DestinationCheckpoint;

    public override void Interact()
    {
        base.Interact();
        myTalk.NewTalk("approach");
        interactedWith = true;
    }

    public override void DoEvent(string key)
    {
        if (key == "use")
        {
            //use door

            //play door sound
            GetComponent<AudioSource>().Play();

            //do door stuff
        }
    }

    public override bool ConditionalChoice(string key)
    {
        //custom checks

        //check for equipped item
        return base.ConditionalChoice(key);

    }
}
