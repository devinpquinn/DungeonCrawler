using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [HideInInspector]
    public bool inRange = false;

    //dialogue assets
    [HideInInspector]
    public RPGTalk myTalk;
    public TextAsset myText;

    [HideInInspector]
    public bool interactedWith = false;

    private void Awake()
    {
        myTalk = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<RPGTalk>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Detector"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Detector"))
        {
            inRange = false;
            PlayerController.LeavingInteractable(this);
        }
    }

    //do the interaction
    public virtual void Interact()
    {
        myTalk.txtToParse = myText;
        myTalk.callback.AddListener(PlayerController.EndInteraction);
    }

    public virtual void StartTalking(string key, UnityEngine.Events.UnityAction call)
    {
        PlayerController.Instance.myState = PlayerController.playerState.Interacting;
        PlayerController.Instance.SetCursor("interact");
        PlayerController.Instance.StopWalkAnimation();
        TooltipUI.HideTooltip_Static();

        //hide inventory
        PlayerController.Instance.inventoryPanelRect.gameObject.SetActive(false);
        PlayerController.Instance.itemDescriptionPanel.SetActive(false);

        //check if there is an equipped item to display
        PlayerController.Instance.UpdateItemThumbnailExternal();

        myTalk.txtToParse = myText;

        myTalk.callback.RemoveAllListeners();
        if (call != null)
        {
            myTalk.callback.AddListener(call);
        }

        myTalk.NewTalk(key);
    }

    //called from text document
    public virtual void DoEvent(string key)
    {
        //do the event designated by the text key
    }

    //check conditions for displaying a certain choice option
    public virtual bool ConditionalChoice(string key)
    {
        if(key.Contains("item:"))
        {
            string itemKey = key.Substring(key.IndexOf("item:") + 5);
            if(PlayerController.GetEquippedItem() == itemKey)
            {
                return true;
            }
        }
        return false;
    }

    public void AddCharacter(RPGTALK.Helper.RPGTalkCharacter charToAdd)
    {
        RPGTALK.Helper.RPGTalkCharacterSettings settings = new RPGTALK.Helper.RPGTalkCharacterSettings();
        settings.character = charToAdd;
        myTalk.characters.Add(settings);
    }
}
