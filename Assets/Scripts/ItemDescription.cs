using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDescription : MonoBehaviour
{
    public string description;

    public void ShowItemTooltip()
    {
        TooltipUI.ShowTooltip_Static(description);
    }

    public void HideItemTooltip()
    {
        TooltipUI.HideTooltip_Static();
    }
}
