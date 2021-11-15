using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipUI : MonoBehaviour
{
    private RectTransform canvasRectTransform;
    private RectTransform backgroundRectTransform;
    private RectTransform rectTransform;
    private TextMeshProUGUI tmp;
    public Vector3 offset;

    private void Awake()
    {
        canvasRectTransform = transform.parent.GetComponent<RectTransform>();
        backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();
        rectTransform = transform.GetComponent<RectTransform>();
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

    private void Update()
    {
        Vector2 anchoredPosition = (Input.mousePosition + offset ) / canvasRectTransform.localScale.x;

        if(anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width)
        {
            //going off screen to the right
            anchoredPosition.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width;
        }
        if(anchoredPosition.y - backgroundRectTransform.rect.height < 0)
        {
            //going off screen at the bottom
            anchoredPosition.y = backgroundRectTransform.rect.height;
        }

        rectTransform.anchoredPosition = anchoredPosition;
    }
}
