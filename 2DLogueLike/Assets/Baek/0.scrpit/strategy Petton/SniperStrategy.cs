using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperStrategy : IWeaponStrategy
{
    public void Shoot()
    {
        GunManager.Instance.Gunsp.GetComponent<SpriteRenderer>().sprite = GunManager.Instance.Guns[3]; // �� ��ũ��Ʈ�� ��Ʈ�� ��
        ObjectPoolBaek.Instance.Player.timerdelay = 2.5f; // �ѵ����� �ֱ�
        ObjectPoolBaek.Instance.Player.Damage = 15; // �� ������
        ObjectPoolBaek.Instance.Player.bulletSpeed = 30;
    }
}
