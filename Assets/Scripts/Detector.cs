using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    [HideInInspector]
    public PlayerController player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            player.interactables.Add(collision.gameObject.GetComponent<Interactable>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            player.interactables.Remove(collision.gameObject.GetComponent<Interactable>());
        }
    }
}
