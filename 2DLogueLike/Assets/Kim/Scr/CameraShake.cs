using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // 카메라 흔들기
    public float ShakeAmount;
    float shakeTime;
    Vector3 initialPosition;
    public void ShakeTime(float time)
    {
        shakeTime = time;
    }
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if(shakeTime > 0)
        {
            transform.position = Random.insideUnitSphere * ShakeAmount;
            shakeTime -= Time.deltaTime;
        }
        else
        {
            shakeTime = 0.0f;
            transform.position = initialPosition;
        }
    }
}
