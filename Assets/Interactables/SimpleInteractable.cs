using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleInteractable : Interactable
{
    public string title;

    public override void Interact()
    {
        base.Interact();
        myTalk.NewTalk(title);
    }
}
