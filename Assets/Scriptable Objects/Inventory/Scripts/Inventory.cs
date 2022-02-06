using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory")]
public class Inventory : ScriptableObject
{
    public List<ItemObject> InventoryItems = new List<ItemObject>();

    public void AddItem(ItemObject item)
    {
        InventoryItems.Add(item);
    }
}
