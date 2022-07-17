using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryStorage : Interactable
{
    public override void Interact()
    {
        base.Interact();
        if (Progress.storage_met)
        {
            myTalk.NewTalk("storage-again");
        }
        else
        {
            myTalk.NewTalk("storage");
            Progress.storage_met = true;
        }
    }
}
