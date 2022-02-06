using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    public string playerScene;
    public float[] playerPosition;
    public string playerEquipped;

    public PlayerData(PlayerController player)
    {
        playerScene = SceneManager.GetActiveScene().name;

        //inventory goes here

        //equipped item goes here
        playerEquipped = PlayerController.GetEquippedItem();

        playerPosition = new float[3];
        playerPosition[0] = player.transform.position.x;
        playerPosition[1] = player.transform.position.y;
        playerPosition[2] = player.transform.position.z;
    }
}
