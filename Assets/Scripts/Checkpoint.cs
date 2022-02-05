using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    public string CheckpointID;

    public void SaveCheckpoint()
    {
        Debug.Log("<b>Saving...</b>");
        PlayerPrefs.SetString("playerScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("playerPositionX", transform.position.x);
        PlayerPrefs.SetFloat("playerPositionY", transform.position.y);
        Debug.Log("<b>Saved!</b>");
    }
}
