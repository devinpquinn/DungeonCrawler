using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHeadManager : MonoBehaviour
{
    private Image image;
    public SpriteRenderer targetSprite;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void FixedUpdate()
    {
        image.sprite = targetSprite.sprite;
    }

}
