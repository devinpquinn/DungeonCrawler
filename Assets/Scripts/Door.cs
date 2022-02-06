using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Interactable
{
    public string DestinationScene;
    public string DestinationCheckpoint;

    public override void Interact()
    {
        base.Interact();
        myTalk.NewTalk("approach");
        interactedWith = true;
    }

    public override void DoEvent(string key)
    {
        if (key == "use")
        {
            //remove player enabling callback
            myTalk.callback.RemoveListener(PlayerController.EndInteraction);

            //use door
            StartCoroutine(DoDoor());
        }
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
        //List<Item> savedInventory = PlayerController.Instance.inventory;
        //string savedItem = PlayerController.GetEquippedItem();

        //fade out
        FadeManager.FadeOut(0.4f);
        yield return new WaitForSeconds(0.4f);

        //load destination scene
        SceneManager.LoadScene(DestinationScene);

        while (SceneManager.GetActiveScene().name != DestinationScene)
        {
            yield return null;
        }

        //set player to correct position
        foreach (GameObject check in GameObject.FindGameObjectsWithTag("Checkpoint"))
        {
            if (check.name.Equals("Checkpoint - " + DestinationCheckpoint))
            {
                PlayerController.Instance.transform.position = check.transform.position;
                break;
            }
        }

        //set player inventory
        //PlayerController.Instance.inventory = savedInventory;
        //PlayerController.EquipItem(savedItem);

        //save game
        PlayerController.Instance.Save();

        //finish playing sound and destroy
        while (GetComponent<AudioSource>().isPlaying)
        {
            yield return null;
        }

        Destroy(gameObject);
    }

    public override bool ConditionalChoice(string key)
    {
        //custom checks

        //check for equipped item
        return base.ConditionalChoice(key);

    }
}
