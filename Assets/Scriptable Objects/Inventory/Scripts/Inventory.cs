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
        for (int i = 0; i < InventoryItems.Count; i++)
        {
            InventoryItems[i].item = database.GetItem[InventoryItems[i].ID];
        }
    }

    public void LoadInventory(PlayerData data)
    {
        InventoryItems = new List<InventorySlot>();
        for (int i = 0; i < data.playerItems.Count; i++)
        {
            InventoryItems.Add(new InventorySlot(data.playerItems[i], database));
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

    public InventorySlot(int _id, ItemDatabaseObject _database)
    {
        ID = _id;
        item = _database.GetItem[ID];
    }
}
