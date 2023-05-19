using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // 카메라 흔들기 수정중

    [SerializeField] Transform target;  // 카메라가 따라갈 대상
    public float smoothing = 5f;  // 카메라 움직임 부드러움 정도

    Vector3 offset;  // 카메라와 대상의 거리 차이
    Vector3 targetCamPos;
    void Init()
    {
        if (target == null)
        {
            // 대상이 지정되지 않은 경우, ObjectPoolBaek 싱글톤 인스턴스에서 플레이어를 찾아 대상으로 설정합니다.
            target = ObjectPoolBaek.Instance.Player.transform;
        }
    }

    void Start()
    {
        Init();
        offset = transform.position - target.position;  // 카메라와 대상의 거리 차이를 계산합니다.
    }

    void FixedUpdate()
    {
        // 목표 카메라 위치를 계산합니다.
        //Vector3 targetCamPos = target.position + offset;
        targetCamPos = target.position + offset;
        // 부드러운 이동을 위해 Lerp 함수를 사용하여 현재 위치에서 목표 위치로 이동합니다.
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }

}