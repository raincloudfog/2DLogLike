using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunStrategy : MonoBehaviour
{
    public void Shoot()
    {
        GunManager.Instance.Gunsp.GetComponent<SpriteRenderer>().sprite = GunManager.Instance.Guns[2]; // �� ��ũ��Ʈ�� ��Ʈ�� ��
        ObjectPoolBaek.Instance.Player.timerdelay = 1f; // �ѵ����� �ֱ�
        ObjectPoolBaek.Instance.Player.Damage = 3; // �� ������
        ObjectPoolBaek.Instance.Player.bulletSpeed = 25;
    }
}
