using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObolHeadstone : Interactable
{
    public GameObject trapdoorHole;

    public bool CheckEquipped()
    {
        string itemCheck = PlayerController.GetEquippedItem();
        if (itemCheck == "Unbreakable Knot" || itemCheck == "Scrawled Notes" || itemCheck == "Hateful Shovel")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void Interact()
    {
        base.Interact();
        if (interactedWith && CheckEquipped())
        {
            myTalk.NewTalk("obol");
        }
        else
        {
            myTalk.NewTalk("obol-first");
            interactedWith = true;
        }
    }

    public override void DoEvent(string key)
    {
        if (key == "checkObol")
        {
            if (CheckEquipped())
            {
                myTalk.NewTalk("obol");
            }
            else
            {
                myTalk.EndTalk();
            }
        }
        else if (key == "dug")
        {
            trapdoorHole.SetActive(true);
            gameObject.layer = 0;
        }
    }
}
