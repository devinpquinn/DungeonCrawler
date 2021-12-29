using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    public string myItemName;

    public void EquipItem()
    {
        if(PlayerController.GetEquippedItem() == myItemName)
        {
            PlayerController.UnequipAllItems();
        }
        else
        {
            PlayerController.EquipItem(myItemName);
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
