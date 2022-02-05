using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    public string CheckpointID;

    public void SaveCheckpoint()
    {
        PlayerPrefs.SetString("playerScene", SceneManager.GetActiveScene().name);
    }
}
