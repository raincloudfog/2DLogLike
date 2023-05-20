using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Item
{
    Collider2D Pick; // 플레이어 감지
    Character Player; // 플레이어
    [SerializeField] Sprite[] Foods; // 음식 이미지들
    [SerializeField] Sprite mainspr; // 기본 음식 이미지
    [SerializeField] protected float ranginPlayer; // 플레이어와 닿였을때
    Rigidbody2D rigid;
    bool sprite = true;


    public void Init()
    {
        if (rigid == null)
        {
            rigid = GetComponent<Rigidbody2D>();
        }
        if (Player == null)
        {
            Player = ObjectPoolBaek.Instance.Player;
        }
        if (GetComponent<SpriteRenderer>().sprite == mainspr)
        {
            if(sprite == false)
            {
                return;
            }
            GetComponent<SpriteRenderer>().sprite = Foods[Random.Range(0, 4)];
            sprite = false;
        }
            
    }
    public override void Pick_Up() // 코인과 같음
    {
        
       
        if (Pick != null)
        {
            //Debug.Log("플레이어 닿임");
            if (GameManager.Instance.Player.Hp == GameManager.Instance.Player.MaxHp)
            {
                Destroy(gameObject);
                return;
            }
            GameManager.Instance.Player.Hp += 1;
            
            Destroy(gameObject);
            
        }
    }

    private void FixedUpdate()
    {
        Init();

        Pick = Physics2D.OverlapCircle(transform.position,
               ranginPlayer,
               LayerMask.GetMask("Player"));
        rigid.velocity = (Player.transform.position - transform.position).normalized * 15f;
        Pick_Up();
        
    }
}
