using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public int Hp;
    public float speed;
    public Collider2D player;
    [SerializeField] protected float ranginPlayer; // 플레이어를 발견하는 범위
     
    public bool isshop = true;
    [SerializeField] GameObject shop;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, ranginPlayer);
        
    }

    private void Update()
    {
        if(player != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                shop.SetActive(true);
                GameManager.Instance.Player.isAttack = false;
            }
        }
        else
        {
            shop.SetActive(false);
            GameManager.Instance.Player.isAttack = true;
        }
    }
    private void FixedUpdate()
    {
        player = Physics2D.OverlapCircle(transform.position, transform.localScale.x * 2, LayerMask.GetMask("Player"));

        if(player != null)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }

    }
    public virtual void Talk()
    {

    }
}
