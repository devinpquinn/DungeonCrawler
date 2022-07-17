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
    public List<int> playerItems;
    public List<string> playerProgress;

    public PlayerData(PlayerController player)
    {
        //scene goes here
        playerScene = SceneManager.GetActiveScene().name;

        //inventory goes here
        playerItems = new List<int>();
        for (int i = 0; i < player.inventory.InventoryItems.Count; i++)
        {
            playerItems.Add(player.inventory.InventoryItems[i].ID);
        }

        //equipped item goes here
        playerEquipped = PlayerController.GetEquippedItem();

        playerPosition = new float[3];
        playerPosition[0] = player.transform.position.x;
        playerPosition[1] = player.transform.position.y;
        playerPosition[2] = player.transform.position.z;

        //progress data goes here
        playerProgress = Progress.data;
    }
}
