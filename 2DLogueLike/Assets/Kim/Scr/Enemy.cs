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
    protected Collider2D col = new Collider2D(); // ���������� �ʼ�
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
    [SerializeField] protected float shotDelay;     // �߻� ������
    [SerializeField] protected float ranginPlayer;  // �÷��̾ �߰��ϴ� ����
    [SerializeField] protected float ranginShot;   // �÷��̾ �߰��Ͽ� ��������ִ� ����

    protected bool isAttack = true; // ������ �ߴ°�

    protected bool isDie = false; // �׾��°�
    protected bool isrealDie = false; // 

    protected bool startCo = true; // �ڷ�ƾ�� �����ߴ°�
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
                // ������ �÷��̾�� ������ ������� ��
            }
        }
    }
}
