using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [HideInInspector]
    public PlayerController player;
    [HideInInspector]
    public bool inRange = false;

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
            if(player.currentInteractable == this)
            {
                player.SetCursor("default");
                player.currentInteractable = null;
            }
        }
    }
}
