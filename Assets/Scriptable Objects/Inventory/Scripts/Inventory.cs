using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory")]
public class Inventory : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemDatabaseObject database;
    public List<InventorySlot> InventoryItems = new List<InventorySlot>();

    public void AddItem(ItemObject item)
    {
        InventoryItems.Add(new InventorySlot(database.GetID[item], item));
    }

    public void OnAfterDeserialize()
    {
        for(int i = 0; i < InventoryItems.Count; i++)
        {
            
        }
    }

    public void OnBeforeSerialize()
    {
        
    }
}

[System.Serializable]
public class InventorySlot
{
    public int ID;
    public ItemObject item;
    public InventorySlot(int _id, ItemObject _item)
    {
        ID = _id;
        item = _item;
    }
}
