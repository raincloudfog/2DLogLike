using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gundrop : Item
{
    
    
  
    public override void Pick_Up()
    {
        

        Debug.Log("�÷��̾� ����");
        GunManager.Instance.waeponType = WaeponType.Missile;
        GunManager.Instance.SetWeaponStrategy(new MissieStrategy()); // ���� ���Ͻ� �ߵ���
        Destroy(gameObject);

        
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))        
            Pick_Up();
    }
}
