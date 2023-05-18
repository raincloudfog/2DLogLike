using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunStrategy : IWeaponStrategy
{
    public void Shoot()
    {
        GunManager.Instance.Gunsp.GetComponent<SpriteRenderer>().sprite = GunManager.Instance.Guns[1]; // �� ��ũ��Ʈ�� ��Ʈ�� ��
        ObjectPoolBaek.Instance.Player.timerdelay = 0.2f; // �ѵ����� �ֱ�
        ObjectPoolBaek.Instance.Player.Damage = 3; // �� ������
        ObjectPoolBaek.Instance.Player.bulletSpeed = 25;
    }
}
