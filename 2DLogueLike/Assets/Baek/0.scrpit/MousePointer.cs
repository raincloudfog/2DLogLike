using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursorview : MonoBehaviour
{
    
    public Texture2D cursorTexture; // 사용할 마우스 커서 텍스처
    public CursorMode cursorMode = CursorMode.Auto; // 마우스 커서의 동작 모드 (기본값: Auto)
    public Vector2 hotSpot = Vector2.zero; // 마우스 커서의 클릭 지점 (기본값: (0, 0))

    

    private void Start()
    {
        // 마우스 커서 텍스처를 설정합니다.
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
}
