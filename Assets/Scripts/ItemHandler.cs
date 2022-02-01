using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    public string myItemName;
    private AudioSource myAudioSource;

    public AudioClip equipSound;
    public AudioClip unequipSound;

    private void Awake()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    public void EquipItem()
    {
        if(PlayerController.GetEquippedItem() == myItemName)
        {
            PlayerController.UnequipAllItems();
            myAudioSource.PlayOneShot(unequipSound);
        }
        else
        {
            PlayerController.EquipItem(myItemName);
            myAudioSource.PlayOneShot(equipSound);
        }
    }

    public void ShowItemDescription()
    {
        PlayerController.ShowItemDescription(myItemName);
    }

    public void HideItemDescription()
    {
        PlayerController.HideItemDescription();
    }
}
