using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointer : MonoBehaviour
{
    private void Update()
    {
        // ���콺 Ŀ���� ��ġ�� �о�ɴϴ�.
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f; // ���콺 Ŀ���� ��� ������ �Ÿ��� �����մϴ�.

        // ī�޶��� ���� ��ǥ�� ��ȯ�մϴ�.
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // ���콺 ������ ��Ҹ� �ش� ��ġ�� �̵���ŵ�ϴ�.
        transform.position = worldPosition;
    }
}