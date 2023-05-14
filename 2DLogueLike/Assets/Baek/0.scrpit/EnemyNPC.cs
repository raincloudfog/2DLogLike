using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNPC : MonoBehaviour
{
    Collider2D Player; // 플레이어 감지

    [SerializeField] int Hp = 50;
    [SerializeField] float speed = 5f;
    [SerializeField] float timer = 0;
    [SerializeField] float delaytimer = 2f;
    [SerializeField] Sprite spr;

    


    private void FixedUpdate()
    {
        Player = Physics2D.OverlapCircle(transform.position, 5f);
        if(Player != null)
        {

        }
    }


    
}


