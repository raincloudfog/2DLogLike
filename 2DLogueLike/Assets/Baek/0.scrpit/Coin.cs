using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    Collider2D Pick;
    Character Player;
    [SerializeField] protected float ranginPlayer; // 플레이어와 닿였을때
    Rigidbody2D rigid;
    public override void Pick_Up()
    {
        if (Pick != null)
        {
            GameManager.Instance.coin++;
            Debug.Log("플레이어 닿임");                       
            Destroy(gameObject);

        }
    }

    private void OnDrawGizmos() // 범위 계산
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

        rigid.velocity = (Player.transform.position - transform.position).normalized * 15f;
        Pick_Up();
    }
}
