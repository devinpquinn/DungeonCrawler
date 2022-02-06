using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemObject : ScriptableObject
{
    public GameObject itemPrefab;
    public string itemName;
    public Sprite itemSprite;
    [TextArea(5, 5)]
    public string itemDescription;
}
