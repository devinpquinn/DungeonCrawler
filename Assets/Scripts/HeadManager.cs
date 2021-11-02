using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadManager : MonoBehaviour
{
    private PlayerController pc;

    private void Awake()
    {
        pc = gameObject.transform.parent.parent.GetComponent<PlayerController>();
    }

    public void ActivateHead()
    {
        pc.ActivateHead();
    }
}
