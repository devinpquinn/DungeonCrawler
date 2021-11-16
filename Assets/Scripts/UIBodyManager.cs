using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBodyManager : MonoBehaviour
{
    private SpriteRenderer sprite;
    public SpriteRenderer targetSprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        sprite.flipX = targetSprite.flipX;
    }
}
