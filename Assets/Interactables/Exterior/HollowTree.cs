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
            myTalk.NewTalk("tree");
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
    }
}
