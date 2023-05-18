using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // ī�޶� ���� ������

    [SerializeField] Transform target;  // ī�޶� ���� ���
    public float smoothing = 5f;  // ī�޶� ������ �ε巯�� ����

    Vector3 offset;  // ī�޶�� ����� �Ÿ� ����
    Vector3 targetCamPos;
    void Init()
    {
        if (target == null)
        {
            // ����� �������� ���� ���, ObjectPoolBaek �̱��� �ν��Ͻ����� �÷��̾ ã�� ������� �����մϴ�.
            target = ObjectPoolBaek.Instance.Player.transform;
        }
    }

    void Start()
    {
        Init();
        offset = transform.position - target.position;  // ī�޶�� ����� �Ÿ� ���̸� ����մϴ�.
    }

    void FixedUpdate()
    {
        // ��ǥ ī�޶� ��ġ�� ����մϴ�.
        //Vector3 targetCamPos = target.position + offset;
        targetCamPos = target.position + offset;
        // �ε巯�� �̵��� ���� Lerp �Լ��� ����Ͽ� ���� ��ġ���� ��ǥ ��ġ�� �̵��մϴ�.
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }

   /* public IEnumerator Shake(float _amount, float _duration)
    {
        float timer = 0;
        while(timer <= _duration)
        {
            transform.position = (Vector3)Random.insideUnitCircle * _amount + targetCamPos;
            timer += Time.deltaTime;
            yield return null;
        }
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }*/
}