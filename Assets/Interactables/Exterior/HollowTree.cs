using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HollowTree : Interactable
{
    public ItemObject gift;
    public Animator treeAnim;
    public RPGTALK.Helper.RPGTalkCharacter myCharacter;

    private void Start()
    {
        AddCharacter(myCharacter);
    }

    public override void Interact()
    {
        base.Interact();
        if (interactedWith)
        {
            if (PlayerController.GetEquippedItem() != null)
            {
                string equippedItem = PlayerController.GetEquippedItem();
                if (equippedItem == "Unbreakable Knot" || equippedItem == "Trail Mix" || equippedItem == "Bitter Berries")
                {
                    myTalk.NewTalk("tree");
                }
                else
                {
                    myTalk.NewTalk("tree-no-item");
                }
            }
            else
            {
                myTalk.NewTalk("tree-no-item");
            }
        }
        else
        {
            myTalk.NewTalk("tree-first");
            interactedWith = true;
        }
    }

    public override void DoEvent(string key)
    {
        if(key == "owlItem")
        {
            PlayerController.AddItem(gift);

            if(PlayerController.GetEquippedItem() == "Trail Mix")
            {
                PlayerController.RemoveItem("Trail Mix");
            }

            treeAnim.Play("hollowTreeNoEyes");
            gameObject.layer = 0;
        }
        else if(key == "checkTreeItem")
        {
            if(PlayerController.GetEquippedItem() != null)
            {
                string equippedItem = PlayerController.GetEquippedItem();
                if(equippedItem == "Unbreakable Knot" || equippedItem == "Trail Mix" || equippedItem == "Bitter Berries")
                {
                    myTalk.NewTalk("tree");
                }
                else
                {
                    myTalk.NewTalk("tree-no-item");
                }
            }
            else
            {
                myTalk.NewTalk("tree-no-item");
            }
        }
    }
}
