using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObject : Interactable
{
    private bool patted = false;
    public Item gift;
    public SpriteRenderer barrelRenderer;
    public Sprite doffedSprite;

    public override void Interact()
    {
        base.Interact();
        if (PlayerController.CheckForItem(gift.itemName))
        {
            myTalk.NewTalk("has-item");
        }
        else
        {
            if (patted)
            {
                myTalk.NewTalk("back-pat");
            }
            else
            {
                if(interactedWith)
                {
                    myTalk.NewTalk("back");
                }
                else
                {
                    myTalk.NewTalk("1st");
                }
            }
        }
        interactedWith = true;
    }

    public override void DoEvent(string key)
    {
        if (key == "item")
        {
            PlayerController.AddItem(gift);
        }
        else if (key == "lid")
        {
            barrelRenderer.sprite = doffedSprite;
        }
    }

    public override bool ConditionalChoice(string key)
    {
        //custom checks

        //check for equipped item
        return base.ConditionalChoice(key);

    }
}
