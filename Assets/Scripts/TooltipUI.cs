using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipUI : MonoBehaviour
{
    private RectTransform backgroundRectTransform;
    private TextMeshProUGUI tmp;

    private void Awake()
    {
        backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();
        tmp = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        SetText("Wooden Door");
    }

    private void SetText(string tooltipText)
    {
        tmp.SetText(tooltipText);
        tmp.ForceMeshUpdate();

        Vector2 textSize = tmp.GetRenderedValues(false);
        Vector2 paddingSize = new Vector2(24, 24);

        backgroundRectTransform.sizeDelta = textSize + paddingSize;
    }
}
