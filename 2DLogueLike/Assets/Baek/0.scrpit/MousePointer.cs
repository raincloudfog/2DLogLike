using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursorview : MonoBehaviour
{
    
    public Texture2D cursorTexture; // ����� ���콺 Ŀ�� �ؽ�ó
    public CursorMode cursorMode = CursorMode.Auto; // ���콺 Ŀ���� ���� ��� (�⺻��: Auto)
    public Vector2 hotSpot = Vector2.zero; // ���콺 Ŀ���� Ŭ�� ���� (�⺻��: (0, 0))

    

    private void Start()
    {
        // ���콺 Ŀ�� �ؽ�ó�� �����մϴ�.
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
}
