using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleDoor : Interactable
{
    public string DestinationScene;
    public string DestinationCheckpoint;

    public float transitionTime = 0.4f;

    public override void Interact()
    {
        base.Interact();
        //remove player enabling callback
        myTalk.callback.RemoveListener(PlayerController.EndInteraction);

        //use door
        StartCoroutine(DoDoor());
    }

    IEnumerator DoDoor()
    {
        //set player busy
        PlayerController.Instance.myState = PlayerController.playerState.Locked;

        //play door sound
        GetComponent<AudioSource>().Play();

        //preserve this object
        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);

        //preserve player info
        string savedItem = PlayerController.GetEquippedItem();
        Inventory savedInventory = PlayerController.Instance.inventory;

        //fade out
        FadeManager.FadeOut(0.4f);
        yield return new WaitForSeconds(transitionTime);

        //load destination scene
        SceneManager.LoadScene(DestinationScene);

        //hide to avoid being hovered over while still active
        transform.localScale = new Vector3(0, 0, 0);

        while (SceneManager.GetActiveScene().name != DestinationScene)
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

        if (DestinationCheckpoint == null)
        {
            playerObject.transform.position = GameObject.FindGameObjectWithTag("Checkpoint").transform.position;
        }
        else
        {
            foreach (GameObject check in GameObject.FindGameObjectsWithTag("Checkpoint"))
            {
                if (check.name.Equals("Checkpoint - " + DestinationCheckpoint))
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
