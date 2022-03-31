using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LadderHole : Interactable
{
    public string destinationScene = null;
    public string destinationCheckpoint = null;

    public AudioClip doorSound;

    public List<AudioSource> audiosToFade;

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
        foreach(AudioSource src in audiosToFade)
        {
            src.gameObject.AddComponent<FadeOutAudio>();
        }

        //preserve this object
        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);

        //preserve player info
        string savedItem = PlayerController.GetEquippedItem();
        Inventory savedInventory = PlayerController.Instance.inventory;

        //fade out
        FadeManager.FadeOut(0.4f);
        yield return new WaitForSeconds(9.5f);

        //load destination scene
        SceneManager.LoadScene(destinationScene);

        while (SceneManager.GetActiveScene().name != destinationScene)
        {
            yield return null;
        }

        //set player to correct position
        if(destinationCheckpoint == null)
        {
            GameObject playerObject = PlayerController.Instance.gameObject;

            playerObject.transform.position = GameObject.FindGameObjectWithTag("Checkpoint").transform.position;

            //reset camera position
            Cinemachine.CinemachineVirtualCamera ccam = playerObject.GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>();
            ccam.enabled = false;
            ccam.enabled = true;
        }
        else
        {
            foreach (GameObject check in GameObject.FindGameObjectsWithTag("Checkpoint"))
            {
                if (check.name.Equals("Checkpoint - " + destinationCheckpoint))
                {
                    GameObject playerObject = PlayerController.Instance.gameObject;

                    playerObject.transform.position = check.transform.position;

                    //reset camera position
                    Cinemachine.CinemachineVirtualCamera ccam = playerObject.GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>();
                    ccam.enabled = false;
                    ccam.enabled = true;

                    break;
                }
            }
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
