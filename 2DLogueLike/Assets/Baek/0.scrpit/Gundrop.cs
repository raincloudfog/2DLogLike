using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gundrop : Item
{
    
    
  
    public override void Pick_Up()
    {
        

        Debug.Log("플레이어 닿임");
        GunManager.Instance.waeponType = WaeponType.Missile;
        GunManager.Instance.SetWeaponStrategy(new MissieStrategy()); // 전략 패턴시 발동됨
        Destroy(gameObject);

        
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))        
            Pick_Up();
    }
}
