using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager _instance;

    //public Texture2D cursor_Position;
    [SerializeField]private Texture2D cursor_Direction;
    [SerializeField] private Texture2D cursor_Normal;
    [SerializeField] private Texture2D cursor_NpcTalk;
    [SerializeField] private Texture2D cursor_Position;

    private void Awake()
    {
        _instance = this;
    }
    public void SetNormal()
    {
        Cursor.visible = true;
        Cursor.SetCursor(cursor_Normal, Vector2.zero, CursorMode.Auto);
    }
    public void SetDirection()
    {
        Cursor.visible = true;
        Cursor.SetCursor(cursor_Direction, Vector2.zero, CursorMode.Auto);
    }
    public void SetPosition()
    {
        Cursor.visible = true;
        Cursor.SetCursor(cursor_Position, Vector2.zero, CursorMode.Auto);
    }
    public void SeNpc()
    {
        Cursor.visible = true;
        Cursor.SetCursor(cursor_NpcTalk, Vector2.zero, CursorMode.Auto);
    }
}
