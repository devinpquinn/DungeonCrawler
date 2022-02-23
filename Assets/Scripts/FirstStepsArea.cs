using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstStepsArea : MonoBehaviour
{
    //dialogue assets
    [HideInInspector]
    public RPGTalk myTalk;
    public TextAsset myText;

    private void Awake()
    {
        myTalk = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<RPGTalk>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Body")
        {
            TookFirstSteps();
        }
    }

    public void TookFirstSteps()
    {
        myTalk.txtToParse = myText;
        myTalk.callback.AddListener(PlayerController.EndInteraction);
        PlayerController.Instance.lockToBody = false;
    }
}
