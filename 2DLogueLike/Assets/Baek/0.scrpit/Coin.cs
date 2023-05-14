using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    Collider2D Pick;
    Character Player;
    [SerializeField] protected float ranginPlayer; // �÷��̾�� �꿴����
    Rigidbody2D rigid;
    public override void Pick_Up()
    {
        if (Pick != null)
        {
            GameManager.Instance.coin++;
            Debug.Log("�÷��̾� ����");                       
            Destroy(gameObject);

        }
    }

    private void OnDrawGizmos() // ���� ���
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, ranginPlayer);
        
    }
    private void FixedUpdate()
    {
        if(rigid == null)
        {
            rigid = GetComponent<Rigidbody2D>();
        }
        if(Player == null)
        {
            Player = ObjectPoolBaek.Instance.Player;
        }

        Pick = Physics2D.OverlapCircle(transform.position,
              ranginPlayer,
              LayerMask.GetMask("Player"));

        rigid.velocity = (Player.transform.position - transform.position).normalized * 5f;
        Pick_Up();
    }
}