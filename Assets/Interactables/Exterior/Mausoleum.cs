using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mausoleum : Interactable
{
    public override void Interact()
    {
        base.Interact();
        if (interactedWith)
        {
            myTalk.NewTalk("maus-again");
        }
        else
        {
            myTalk.NewTalk("maus");
            interactedWith = true;
        }
    }

    public override void DoEvent(string key)
    {
        if(key == "checkMaus")
        {
            gameObject.name = "Mouse-oleum";
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
