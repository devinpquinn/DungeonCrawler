using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyManager : MonoBehaviour
{
    private PlayerController pc;

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
}
