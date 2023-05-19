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
            GameManager.Instance.coin++; // 코인 갯수 증가
            //Debug.Log("플레이어 닿임");                       
            Destroy(gameObject); // 코인은 많이 생성하지않아서 디스트로이 사용

        }
    }

    /*private void OnDrawGizmos() // 범위 계산
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, ranginPlayer);
        
    }*/
    private void FixedUpdate()
    {
        if(rigid == null) // 리지드 바디가 없을경우 
        {
            rigid = GetComponent<Rigidbody2D>();
        }
        if(Player == null) // 플레이어가 없을경우
        {
            Player = ObjectPoolBaek.Instance.Player;
        }

        Pick = Physics2D.OverlapCircle(transform.position, // 플레이어 감지
              ranginPlayer,
              LayerMask.GetMask("Player"));

        rigid.velocity = (Player.transform.position - transform.position).normalized * 15f; // 플레이어를 향해 움직임.
        Pick_Up();
    }
}
