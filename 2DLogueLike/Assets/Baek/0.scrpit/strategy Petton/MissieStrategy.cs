using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissieStrategy : IWeaponStrategy
{
    
    public void Shoot()
    {
        GunManager.Instance.Gunsp.GetComponent<SpriteRenderer>().sprite = GunManager.Instance.Guns[4]; // 총 스크립트는 피트톨 로
        ObjectPoolBaek.Instance.Player.timerdelay = 3.5f; // 총딜레이 주기
        ObjectPoolBaek.Instance.Player.Damage = 30; // 총 데미지
        ObjectPoolBaek.Instance.Player.bulletSpeed = 30;
    }
}
