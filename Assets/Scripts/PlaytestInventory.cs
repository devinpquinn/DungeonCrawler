using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaytestInventory : MonoBehaviour
{
    public List<ItemObject> items;

    private void Start()
    {
        foreach(ItemObject i in items)
        {
            PlayerController.AddItem(i);
        }
    }
}
