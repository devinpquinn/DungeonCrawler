using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PregameManager : MonoBehaviour
{
    public GameObject firstCanvas;
    public GameObject secondCanvas;
    public Button cardButton;

    public void SecondCanvas()
    {
        firstCanvas.SetActive(false);
        secondCanvas.SetActive(true);
    }

    public void DrewStar()
    {
        cardButton.onClick.RemoveAllListeners();
        cardButton.enabled = false;
        secondCanvas.GetComponent<Animator>().Play("drewStar");
    }
}
