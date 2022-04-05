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

    public GameObject audioObject;

    private void Start()
    {
        SetCursor("default");
        firstCanvas.SetActive(true);
        secondCanvas.SetActive(false);
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
        cardButton.onClick.AddListener(DrewEmpress);
        cardButton.gameObject.SetActive(false);
        SetCursor("default");
        secondCanvas.GetComponent<Animator>().Play("drewStar");
    }

    public void StarAnimation()
    {
        starAnim.Play("starAnimated");
    }

    public void DrewEmpress()
    {
        cardButton.onClick.RemoveAllListeners();
        cardButton.gameObject.SetActive(false);
        SetCursor("default");
        secondCanvas.GetComponent<Animator>().Play("drewEmpress");
    }

    public void EmpressAnimation()
    {
        empressAnim.Play("empressAnimated");
    }

    public void UpdateTopText(string newText)
    {
        topText.text = newText;
    }

    public void UpdateBottomText(string newText)
    {
        bottomText.text = newText;
    }

    public void EndPregame()
    {
        StartCoroutine(DoEndPregame());
    }

    IEnumerator DoEndPregame()
    {
        FadeManager.FadeOut(0.4f);
        audioObject.AddComponent<FadeOutAudio>();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Exterior");
    }
}
