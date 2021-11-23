using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Sprite itemSprite;
    public string itemName;
    [TextArea]
    public string itemDescription;

    public void ShowItemTooltip()
    {
        TooltipUI.ShowTooltip_Static(itemDescription);
    }

    public void HideItemTooltip()
    {
        TooltipUI.HideTooltip_Static();
    }
}
