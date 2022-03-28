using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mausoleum : Interactable
{
    public override void Interact()
    {
        base.Interact();
        myTalk.NewTalk("maus");
    }

    public override void DoEvent(string key)
    {
        if(key == "checkMaus")
        {
            if(PlayerController.GetEquippedItem() != null && PlayerController.GetEquippedItem() == "Unbreakable Knot")
            {
                myTalk.NewTalk("maus-q");
            }
            else
            {
                myTalk.EndTalk();
            }
        }
    }
}
