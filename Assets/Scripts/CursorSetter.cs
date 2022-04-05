using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSetter : MonoBehaviour
{
    public PregameManager pm;

    public void SetDefault()
    {
        pm.SetCursor("default");
    }

    public void SetInteract()
    {
        pm.SetCursor("interact");
    }
}
