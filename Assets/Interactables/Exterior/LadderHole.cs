using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LadderHole : Interactable
{
    public string destinationScene = null;
    public string destinationCheckpoint = null;

    public AudioClip doorSound;

    public GameObject audioToFade;

    public override void Interact()
    {
        base.Interact();
        if (interactedWith)
        {
            myTalk.NewTalk("hole");
        }
        else
        {
            myTalk.NewTalk("hole-first");
            interactedWith = true;
        }
    }

    public override void DoEvent(string key)
    {
        if(key == "sceneTransition")
        {
            myTalk.callback.RemoveAllListeners();
            myTalk.EndTalk();
            StartCoroutine(DoDoor());
        }
    }

    IEnumerator DoDoor()
    {
        //set player busy
        PlayerController.Instance.myState = PlayerController.playerState.Locked;
        PlayerController.Instance.SetCursor("default");

        //play door sound
        GetComponent<AudioSource>().clip = doorSound;
        GetComponent<AudioSource>().spatialBlend = 0;
        GetComponent<AudioSource>().Play();

        //fade out other audios
        audioToFade.GetComponent<Animator>().enabled = true;

        //preserve this object
        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);

        //preserve player info
        string savedItem = PlayerController.GetEquippedItem();
        Inventory savedInventory = PlayerController.Instance.inventory;

        //fade out
        FadeManager.FadeOut(0.4f);
        yield return new WaitForSeconds(10f);

        //load destination scene
        SceneManager.LoadScene(destinationScene);

        //hide to avoid being hovered over while still active
        transform.localScale = new Vector3(0, 0, 0);

        while (SceneManager.GetActiveScene().name != destinationScene)
        {
            yield return null;
        }

        //avoid duplicate audio listener
        gameObject.GetComponentInChildren<AudioListener>().enabled = false;

        //set player to correct position
        GameObject playerObject = PlayerController.Instance.gameObject;
        Cinemachine.CinemachineVirtualCamera ccam = playerObject.GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>();
        ccam.enabled = false;

        if (PlayerController.Instance.staticCamera)
        {
            ccam.transform.parent.SetParent(null);
        }

        if (destinationCheckpoint == null)
        {
            playerObject.transform.position = GameObject.FindGameObjectWithTag("Checkpoint").transform.position;
        }
        else
        {
            foreach (GameObject check in GameObject.FindGameObjectsWithTag("Checkpoint"))
            {
                if (check.name.Equals("Checkpoint - " + destinationCheckpoint))
                {
                    playerObject.transform.position = check.transform.position;

                    break;
                }
            }
        }

        //reset camera position
        if (!PlayerController.Instance.staticCamera)
        {
            ccam.enabled = true;
        }

        //set player inventory
        PlayerController.Instance.inventory = savedInventory;
        PlayerController.EquipItem(savedItem);

        //save game
        PlayerController.Instance.Save();

        //finish playing sound and destroy
        while (GetComponent<AudioSource>().isPlaying)
        {
            yield return null;
        }

        Destroy(gameObject);
    }
}
