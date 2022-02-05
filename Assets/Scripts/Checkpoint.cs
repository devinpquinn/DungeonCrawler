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
        Debug.Log("<b>Saved!</b>");
    }
}
