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
        if ((PlayerController.GetEquippedItem() != null) && (PlayerController.GetEquippedItem() == "Unbreakable Knot" || PlayerController.GetEquippedItem() == "Iron Key"))
        {
            if (interactedWith)
            {
                myTalk.NewTalk("gate-item");
            }
            else
            {
                myTalk.NewTalk("gate-item-first");
            }
        }
        else
        {
            myTalk.NewTalk("gate-no-item");
        }
        interactedWith = true;
    }

    public override void DoEvent(string key)
    {
        if(key == "openGate")
        {
            gateSpriteRenderer.sprite = openGateSprite;
            gateCollider.enabled = false;
            gateAudioSource.Play();
            PlayerController.RemoveItem("Iron Key");
            this.gameObject.layer = 0;
        }
    }
}
