using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryLadder : Interactable
{
    public override void Interact()
    {
        base.Interact();
        if (interactedWith)
        {
            myTalk.NewTalk("ladder-again");
        }
        else
        {
            myTalk.NewTalk("ladder");
            interactedWith = true;
        }
    }
}
