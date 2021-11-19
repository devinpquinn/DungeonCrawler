using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [HideInInspector]
    public bool inRange = false;

    //dialogue assets
    public TextAsset myText;
    [HideInInspector]
    public RPGTalk myTalk;

    //state management
    public bool advanceStateAutomatically = false;
    public string[] startKeys;
    [HideInInspector]
    public int stateIndex = 0;

    //events
    public List<UnityEvent> myEvents;

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

    public void Interact()
    {
        myTalk.txtToParse = myText;
        myTalk.NewTalk(startKeys[stateIndex]);

        myTalk.callback.AddListener(PlayerController.EndInteraction);

        if (advanceStateAutomatically)
        {
            IncrementState();
        } 
    }

    public void SetState(int stateKey)
    {
        stateIndex = stateKey;
    }

    public void IncrementState()
    {
        stateIndex++;
    }
}
