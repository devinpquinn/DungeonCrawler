using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [HideInInspector]
    public bool inRange = false;

    //dialogue assets
    public TextAsset myText;
    private RPGTalk myTalk;
    private string startKey = "1";

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

    }
}
