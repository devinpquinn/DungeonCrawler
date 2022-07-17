using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartDoor : Interactable
{
    public string firstScene;
    public GameObject menuTexts;
    public MenuScript ms;

    public override void Interact()
    {
        myTalk.txtToParse = myText;
        string path = Application.persistentDataPath + "/savedata.dev";
        if (System.IO.File.Exists(path))
        {
            myTalk.NewTalk("warning");
            menuTexts.SetActive(false);
        }
        else
        {
            StartCoroutine(DoStart());
        }
    }

    public override void DoEvent(string key)
    {
        if (key == "start")
        {
            SaveSystem.ResetPlayer();
            StartCoroutine(DoStart());
        }
        else if (key == "back")
        {
            PlayerController.Instance.myState = PlayerController.playerState.Immobilized;
            PlayerController.RefocusCam();
            menuTexts.SetActive(true);
        }
    }

    IEnumerator DoStart()
    {
        FadeManager.FadeOut(1f);
        ms.FadeOutAudio();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(firstScene);
    }
}
