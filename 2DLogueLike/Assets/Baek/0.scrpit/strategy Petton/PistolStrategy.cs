using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolStrategy : IWeaponStrategy
{
    public void Shoot()
    {
        GunManager.Instance.Gunsp.GetComponent<SpriteRenderer>().sprite = GunManager.Instance.Guns[0]; // �� ��ũ��Ʈ�� ��Ʈ�� ��
        ObjectPoolBaek.Instance.Player.timerdelay = 0.5f; // �ѵ����� �ֱ�
        ObjectPoolBaek.Instance.Player.Damage = 5; // �� ������
        ObjectPoolBaek.Instance.Player.bulletSpeed = 25;
    }
}
