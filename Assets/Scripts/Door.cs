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

        DontDestroyOnLoad(this);

        //fade out
        FadeManager.FadeOut(0.4f);
        yield return new WaitForSeconds(0.4f);

        //load destination scene
        SceneManager.LoadScene(DestinationScene);

        while (SceneManager.GetActiveScene().name != DestinationScene)
        {
            yield return null;
        }

        //when new scene is loaded, look for correct destination
        GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        Checkpoint targetCheckpoint = null;
        foreach(GameObject check in checkpoints)
        {
            if (check.GetComponent<Checkpoint>().CheckpointID.Equals(DestinationCheckpoint))
            {
                targetCheckpoint = check.GetComponent<Checkpoint>();
                break;
            }
        }

        //found correct checkpoint: save
        targetCheckpoint.SaveCheckpoint();

        //...and load that checkpoint
        PlayerController.Load();

        //shouldn't need this door anymore
        Destroy(gameObject);
    }

    public override bool ConditionalChoice(string key)
    {
        //custom checks

        //check for equipped item
        return base.ConditionalChoice(key);

    }
}
