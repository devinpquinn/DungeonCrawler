using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CemeteryGate : Interactable
{
    public SpriteRenderer gateSpriteRenderer;
    public Sprite openGateSprite;
    public Collider2D gateCollider;
    public AudioSource gateAudioSource;

    public override void Interact()
    {
        base.Interact();
        myTalk.NewTalk("gate-first");
    }

    public override void DoEvent(string key)
    {
        if(key == "checkForKey")
        {
            if(PlayerController.GetEquippedItem() != null)
            {
                myTalk.NewTalk("gate-item");
            }
            else
            {
                myTalk.NewTalk("gate-no-item");
            }
        }
        else if(key == "openGate")
        {
            gateSpriteRenderer.sprite = openGateSprite;
            gateCollider.enabled = false;
            gateAudioSource.Play();
            PlayerController.RemoveItem("Iron Key");
            this.gameObject.layer = 0;
        }
    }
}
