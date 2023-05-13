using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] Transform target;  // ī�޶� ���� ���
    public float smoothing = 5f;  // ī�޶� ������ �ε巯�� ����

    Vector3 offset;  // ī�޶�� ����� �Ÿ� ����

    void Init()
    {
        if(target == null)
        {
            target = ObjectPoolBaek.Instance.Player.transform;
        }
    }
    void Start()
    {
        Init();
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
