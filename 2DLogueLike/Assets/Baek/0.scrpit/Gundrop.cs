using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gundrop : Item
{
    
    
  
    public override void Pick_Up()
    {
        
        
        //Debug.Log("�÷��̾� ����");
        GunManager.Instance.WeaponType = WeaponType.Missile;// �ǸŴ����� ���� Ÿ�Թٲ���
        GunManager.Instance.SetWeaponStrategy(new MissieStrategy()); // �ǸŴ����� ���� ������ �̻��Ϸ� �ٲ���.
        Destroy(gameObject); // �̻����� �ϳ��� ������ƮǮ��������.

        
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))         // ���� �÷��̾�� �������� �Ⱦ�
            Pick_Up();
    }
}
