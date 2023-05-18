using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperStrategy : IWeaponStrategy
{
    public void Shoot()
    {
        GunManager.Instance.Gunsp.GetComponent<SpriteRenderer>().sprite = GunManager.Instance.Guns[3]; // 총 스크립트는 피트톨 로
        ObjectPoolBaek.Instance.Player.timerdelay = 2.5f; // 총딜레이 주기
        ObjectPoolBaek.Instance.Player.Damage = 15; // 총 데미지
        ObjectPoolBaek.Instance.Player.bulletSpeed = 30;
    }
}
