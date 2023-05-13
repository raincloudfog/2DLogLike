using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Item
{
    Collider2D Pick;
    Character Player;
    [SerializeField] Sprite[] Foods;
    [SerializeField] Sprite mainspr;
    [SerializeField] protected float ranginPlayer; // 플레이어와 닿였을때
    Rigidbody2D rigid;
    bool sprite = true;
    private void OnEnable()
    {
        Init();
    }
    private void Start()
    {
        Init();
    }


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
    public override void Pick_Up()
    {
        
        //Init();
        if (Pick != null)
        {
            Debug.Log("플레이어 닿임");
            GameManager.Instance.Player.Hp += 1;
            if(GameManager.Instance.Player.Hp >= GameManager.Instance.Player.Hpcut)
            {
                GameManager.Instance.Player.Hp = GameManager.Instance.Player.Hpcut;
            }
            Destroy(gameObject);
            
        }
    }

    private void FixedUpdate()
    {
        Init();
        

        Pick = Physics2D.OverlapCircle(transform.position,
               ranginPlayer,
               LayerMask.GetMask("Player"));
        rigid.velocity = (Player.transform.position - transform.position).normalized * 5f;
        Pick_Up();
        
    }
}
