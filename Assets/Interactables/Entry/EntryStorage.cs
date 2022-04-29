using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryStorage : Interactable
{
    public override void Interact()
    {
        base.Interact();
        if (interactedWith)
        {
            myTalk.NewTalk("storage-again");
        }
        else
        {
            myTalk.NewTalk("storage");
            interactedWith = true;
        }
    }
}
