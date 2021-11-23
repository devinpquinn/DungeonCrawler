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
        GameObject thisPrefab = Instantiate(itemPrefab, contentHolder);
        thisPrefab.transform.Find("Image").GetComponent<Image>().sprite = thisItem.itemSprite;
        thisPrefab.transform.Find("Name").GetComponent<TextMeshProUGUI>().SetText(thisItem.itemName);
        Item newItem = thisPrefab.AddComponent<Item>();
        newItem = thisItem;
        Destroy(thisItem.gameObject);
        items.Add(newItem);
    }
}
