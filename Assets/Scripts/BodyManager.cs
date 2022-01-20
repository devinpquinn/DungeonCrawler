using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyManager : MonoBehaviour
{
    private PlayerController pc;

    //footstep sounds

    private void Awake()
    {
        pc = gameObject.transform.parent.GetComponent<PlayerController>();
    }

    public void LightOut()
    {
        pc.LightOut();
    }

    public void FreeLight()
    {
        pc.FreeLight();
    }

    public void LightIn()
    {
        pc.LightIn();
    }

    public void ActivateBody()
    {
        pc.ActivateBody();
    }

    public void FootstepSound()
    {
        //play material-appropriate footstep sound
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check for footstep material
        //set footstep sound
    }
}
