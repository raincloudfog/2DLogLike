using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunStrategy : IWeaponStrategy
{
    public void Shoot()
    {
        GunManager.Instance.Gunsp.GetComponent<SpriteRenderer>().sprite = GunManager.Instance.Guns[1]; // 총 스크립트는 피트톨 로
        ObjectPoolBaek.Instance.Player.timerdelay = 0.2f; // 총딜레이 주기
        ObjectPoolBaek.Instance.Player.Damage = 3; // 총 데미지
        ObjectPoolBaek.Instance.Player.bulletSpeed = 25;
    }
}
