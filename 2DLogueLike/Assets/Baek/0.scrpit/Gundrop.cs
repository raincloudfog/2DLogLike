using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gundrop : Item
{
    
    
  
    public override void Pick_Up()
    {
        
        
        //Debug.Log("플레이어 닿임");
        GunManager.Instance.WeaponType = WeaponType.Missile;// 건매니저의 웨폰 타입바꿔줌
        GunManager.Instance.SetWeaponStrategy(new MissieStrategy()); // 건매니저의 웨폰 전략을 미사일로 바꿔줌.
        Destroy(gameObject); // 미사일은 하나라 오브젝트풀하지않음.

        
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))         // 만약 플레이어와 닿았을경우 픽업
            Pick_Up();
    }
}
