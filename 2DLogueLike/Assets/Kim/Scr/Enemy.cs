using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected enum State
    {
        Idle,
        Patrol,
        Attack
    }

    [SerializeField] protected AudioClip audioHit;
    [SerializeField] protected AudioClip enemyBulletShot;
    [SerializeField] protected AudioClip enemyDie;

    [SerializeField] protected LayerMask playerLayer;
    protected State curState = State.Idle;
    //protected Transform player;
    protected Animator anim;
    protected Rigidbody2D rigid;
    protected AudioSource audio;
    protected Collider2D col = new Collider2D(); // 감지범위는 필수
    [SerializeField] protected int enemyHp;
    [SerializeField] protected int curEnemyHp;
    [SerializeField] protected int enemySpeed;
    [SerializeField] protected int curEnemySpeed;
    [SerializeField] protected int enemyBulletSpeed;
    [SerializeField] protected int curEnemyBulletSpeed;
    [SerializeField] protected int enemybulletDamage;
    [SerializeField] protected int curEnemyBulletDamage;
    [SerializeField] protected int curBodyDamage;
    [SerializeField] protected int bodyDamage;
    [SerializeField] protected float shotDelay;     // 발사 딜레이
    [SerializeField] protected float ranginPlayer;  // 플레이어를 발견하는 범위
    [SerializeField] protected float ranginShot;   // 플레이어를 발견하여 총을쏠수있는 범위

    protected bool isAttack = true; // 공격을 했는가

    protected bool isDie = false; // 죽었는가
    protected bool isrealDie = false; // 

    protected bool startCo = true; // 코루틴을 시작했는가
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, ranginPlayer);
        Gizmos.DrawWireSphere(transform.position, ranginShot);
    }
    protected virtual void Init()
    {
        if(anim == null)
            anim = GetComponent<Animator>();
        if (rigid == null)
            rigid = GetComponent<Rigidbody2D>();
        if (audio == null)
            audio = GetComponent<AudioSource>();
    }

    public void IsHit(int damage)
    {
        if (isDie == false)
        {
            curEnemyHp -= damage;
            anim.SetTrigger("isHit");
            if(curEnemyHp <= 0)
            {
                PlaySound("isDie");
                isDie = true;
            }
        }
    }
    
    protected virtual void SetState(State newState)
    {   
        curState = newState;
    }

    protected virtual void PlaySound(string action)
    {
        switch (action)
        {
            case "isHit":
                audio.clip = audioHit;
                break;
            case "isShot":
                audio.clip = enemyBulletShot;
                break;
            case "isDie":
                audio.clip = enemyDie;
                break;
        }
        audio.Play();
    }

    protected void OnCollisionStay2D(Collision2D collision)
    {
        if (isDie == false)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<Character>().PlayerHIt(1);
                // 적들이 플레이어에게 닿으면 대미지를 줌
            }
        }
    }
}
