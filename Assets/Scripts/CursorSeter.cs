using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSeter : MonoBehaviour
{
    [SerializeField]
    Texture2D cursor;
    float xspot;
    float yspot;

    private void Start()
    {
        cursorSet(cursor);
    }

    void cursorSet(Texture2D tex)
    {
        CursorMode mode = CursorMode.Auto;
        xspot = tex.width / 2;
        yspot = tex.height / 2;
        Vector2 hotSpot = new Vector2(xspot, yspot);
        Cursor.SetCursor(tex, hotSpot, mode);
    }
}
