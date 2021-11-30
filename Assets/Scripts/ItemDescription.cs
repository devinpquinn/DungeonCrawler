using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDescription : MonoBehaviour
{
    public string myItemName;

    public void ShowItemDescription()
    {
        PlayerController.ShowItemDescription(myItemName);
    }

    public void HideItemDescription()
    {
        PlayerController.HideItemDescription();
    }
}
