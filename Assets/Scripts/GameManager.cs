using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Texture2D mouseDefault;

    private void Awake()
    {
        Cursor.SetCursor(mouseDefault, new Vector2(0, 0), CursorMode.Auto);
    }
}
