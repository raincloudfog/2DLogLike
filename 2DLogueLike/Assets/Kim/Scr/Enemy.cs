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
    [SerializeField] protected LayerMask playerLayer;
    protected State curState = State.Idle;
    //protected Transform player;
    protected Animator anim;
    protected Rigidbody2D rigid;
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
    [SerializeField] protected float shotDelay;
    [SerializeField] protected float ranginPlayer; // 플레이어를 발견하는 범위
    [SerializeField] protected float ranginShot; // 플레이어를 발견하여 총을쏠수있는 범위

    protected bool isAttack = true;

    protected bool isDie = false;
    protected bool isrealDie = false;
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
    }

    public void IsHit(int damage)
    {
        if (isDie == false)
        {
            curEnemyHp -= damage;
            anim.SetTrigger("isHit");
            if(curEnemyHp <= 0)
            {
                isDie = true;
            }
        }
    }
    
    protected virtual void SetState(State newState)
    {   
        curState = newState;
    }
}
