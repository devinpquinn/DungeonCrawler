using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryLadder : Interactable
{
    public override void Interact()
    {
        base.Interact();
        if (Progress.ladder_met)
        {
            myTalk.NewTalk("ladder-again");
        }
        else
        {
            myTalk.NewTalk("ladder");
            Progress.ladder_met = true;
        }
    }
}
