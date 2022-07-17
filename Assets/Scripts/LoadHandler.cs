using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadHandler : MonoBehaviour
{
    public void Load()
    {
        StartCoroutine(LoadGame());
    }

    IEnumerator LoadGame()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        DontDestroyOnLoad(gameObject);

        PlayerController oldPlayer = PlayerController.Instance;

        //set player scene
        SceneManager.LoadScene(data.playerScene);

        while (SceneManager.GetActiveScene().name != data.playerScene || PlayerController.Instance == oldPlayer)
        {
            yield return null;
        }

        PlayerController newPlayer = PlayerController.Instance;
        Cinemachine.CinemachineVirtualCamera ccam = newPlayer.GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>();

        if (newPlayer.staticCamera)
        {
            newPlayer.GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>().transform.parent.SetParent(null);
        }

        //set player position
        Vector3 playerPos;
        playerPos.x = data.playerPosition[0];
        playerPos.y = data.playerPosition[1];
        playerPos.z = data.playerPosition[2];

        newPlayer.transform.position = playerPos;

        //reset camera position
        if(ccam.enabled == true && !newPlayer.staticCamera)
        {
            ccam.enabled = false;
            ccam.enabled = true;
        }

        //set player inventory
        newPlayer.inventory.LoadInventory(data);

        //set equipped item
        PlayerController.EquipItem(data.playerEquipped);

        //set progress values
        Progress.data = data.playerProgress;

        //when done, destroy this
        Destroy(gameObject);
    }
}
