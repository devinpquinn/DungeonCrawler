using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PregameManager : MonoBehaviour
{
    public GameObject firstCanvas;
    public GameObject secondCanvas;
    public Button cardButton;

    public Animator starAnim;
    public Animator empressAnim;

    public TextMeshProUGUI topText;
    public TextMeshProUGUI bottomText;

    public Texture2D cursorDefault;
    public Texture2D cursorInteract;

    private void Start()
    {
        SetCursor("default");
    }

    public void SetCursor(string state)
    {
        if (state == "default")
        {
            Cursor.SetCursor(cursorDefault, new Vector2(0, 0), CursorMode.Auto);
        }
        else if (state == "interact")
        {
            Cursor.SetCursor(cursorInteract, new Vector2(0, 0), CursorMode.Auto);
        }
    }

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

    public void StarAnimation()
    {
        starAnim.Play("starAnimated");
    }

    public void EmpressAnimation()
    {
        starAnim.Play("empressAnimated");
    }

    public void UpdateTopText(string newText)
    {
        topText.text = newText;
    }

    public void UpdateBottomText(string newText)
    {
        bottomText.text = newText;
    }
}
