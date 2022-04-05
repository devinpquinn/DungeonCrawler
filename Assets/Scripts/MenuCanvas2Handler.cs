using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvas2Handler : MonoBehaviour
{
    public PregameManager pm;

    public void DoStarAnimation()
    {
        pm.StarAnimation();
    }

    public void Interesting()
    {
        pm.UpdateBottomText("Interesting.");
    }

    public void DrawAgain()
    {
        pm.UpdateTopText("Draw another.");
    }
}
