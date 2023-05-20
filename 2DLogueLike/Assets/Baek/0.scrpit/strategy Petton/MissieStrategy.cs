using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissieStrategy : IWeaponStrategy
{
    
    public void Shoot()
    {
        GunManager.Instance.Gunsp.GetComponent<SpriteRenderer>().sprite = GunManager.Instance.Guns[4]; // �� ��ũ��Ʈ�� ��Ʈ�� ��
        ObjectPoolBaek.Instance.Player.timerdelay = 3.5f; // �ѵ����� �ֱ�
        ObjectPoolBaek.Instance.Player.Damage = 30; // �� ������
        ObjectPoolBaek.Instance.Player.bulletSpeed = 30;
    }
}
