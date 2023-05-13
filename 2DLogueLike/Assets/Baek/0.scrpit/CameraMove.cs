using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] Transform target;  // 카메라가 따라갈 대상
    public float smoothing = 5f;  // 카메라 움직임 부드러움 정도

    Vector3 offset;  // 카메라와 대상의 거리 차이

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
