using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private Collider2D c;

    private void Awake()
    {
        c = GetComponent<Collider2D>();
    }

    private void OnMouseEnter()
    {
        
    }
}
