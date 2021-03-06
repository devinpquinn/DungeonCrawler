using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Shovel : Interactable
{
    public SpriteRenderer shovelSpriteRenderer;
    public ItemObject shovelItem;

    public override void Interact()
    {
        base.Interact();
        if (interactedWith)
        {
            myTalk.NewTalk("shovel");
        }
        else
        {
            interactedWith = true;
            myTalk.NewTalk("shovel-first");
        }
    }

    public override void DoEvent(string key)
    {
        if(key == "tookShovel")
        {
            //edit original shovel object
            shovelSpriteRenderer.enabled = false;
            shovelSpriteRenderer.gameObject.GetComponent<ShadowCaster2D>().enabled = false;
            shovelSpriteRenderer.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            this.gameObject.layer = 0;

            PlayerController.AddItem(shovelItem);
        }
    }
}
