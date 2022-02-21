using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueDoor : Interactable
{
    public GameObject chains;

    private void Start()
    {
        string path = Application.persistentDataPath + "/savedata.dev";
        if (System.IO.File.Exists(path))
        {
            chains.SetActive(false);
        }
        else
        {
            chains.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public override void Interact()
    {
        StartCoroutine(DoContinue());
    }

    IEnumerator DoContinue()
    {
        FadeManager.FadeOut(1f);
        yield return new WaitForSeconds(1f);
        PlayerController.Instance.Load();
    }
}
