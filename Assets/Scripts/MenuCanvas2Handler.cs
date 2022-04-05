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

    public void DoEmpressAnimation()
    {
        pm.EmpressAnimation();
    }

    public void Interesting()
    {
        pm.UpdateBottomText("Interesting.");
    }

    public void DrawAgain()
    {
        pm.UpdateTopText("Draw another.");
        pm.cardButton.gameObject.SetActive(true);
    }

    public void TheEmpress()
    {
        pm.UpdateBottomText("The Empress.");
    }

    public void VeryWell()
    {
        pm.UpdateTopText("Very well.");
    }

    public void LetsBegin()
    {
        pm.UpdateBottomText("Let us begin.");
    }

    public void DoEndPregame()
    {
        pm.EndPregame();
    }
}
