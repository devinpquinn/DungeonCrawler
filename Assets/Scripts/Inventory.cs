using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    public List<Item> items; //the player's current inventory
    public Transform contentHolder; //the UI parent of the inventory items displayed
    public GameObject itemPrefab; //the prefab that is used to display an item in the UI

    public void AddItem(Item thisItem)
    {
        
    }
}
