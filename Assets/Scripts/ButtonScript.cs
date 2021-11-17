using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonScript : MonoBehaviour
{
    private TextMeshProUGUI tmp;

    private void Awake()
    {
        tmp = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void StartHighlight()
    {
        tmp.color = Color.yellow;
    }

    public void EndHighlight()
    {
        tmp.color = Color.white;
    }
}
