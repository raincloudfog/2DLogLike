using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointer : MonoBehaviour
{
    private void Update()
    {
        // 마우스 커서의 위치를 읽어옵니다.
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f; // 마우스 커서와 요소 사이의 거리를 설정합니다.

        // 카메라의 월드 좌표로 변환합니다.
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // 마우스 포인터 요소를 해당 위치로 이동시킵니다.
        transform.position = worldPosition;
    }
}