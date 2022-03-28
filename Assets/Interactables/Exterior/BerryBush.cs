using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryBush : Interactable
{
    public ItemObject gift;
    public SpriteRenderer bushRenderer;
    public Sprite bushAfter;

    public override void Interact()
    {
        base.Interact();
        myTalk.NewTalk("berries");
    }

    public override void DoEvent(string key)
    {
        if(key == "berryItem")
        {
            gameObject.layer = 0;
            PlayerController.AddItem(gift);
            bushRenderer.sprite = bushAfter;
        }
    }
}
