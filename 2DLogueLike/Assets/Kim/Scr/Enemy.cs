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
    protected Vector3 localScale;
    protected Vector2 offset;
    [SerializeField] protected AudioClip audioHit;
    [SerializeField] protected AudioClip enemyBulletShot;
    [SerializeField] protected AudioClip enemyDie;

    [SerializeField] protected LayerMask playerLayer;

    protected State curState = State.Idle;
    protected Animator anim;
    protected Rigidbody2D rigid;
    protected AudioSource audio;
    protected CapsuleCollider2D capCol;
    protected Collider2D col = new Collider2D(); // ���������� �ʼ�

    [SerializeField] protected int enemyHp;
    [SerializeField] protected int curEnemyHp;
    [SerializeField] protected int enemySpeed;
    [SerializeField] protected int curEnemySpeed;
    [SerializeField] protected int enemyBulletSpeed;
    [SerializeField] protected int curEnemyBulletSpeed;
  
    [SerializeField] protected float shotDelay;     // �߻� ������
    [SerializeField] protected float ranginPlayer;  // �÷��̾ �߰��ϴ� ����
    [SerializeField] protected float ranginShot;   // �÷��̾ �߰��Ͽ� ��������ִ� ����

    protected bool isAttack = true; // ������ �ߴ°�

    protected bool isDie = false; // �׾��°�
    protected bool isrealDie = false; 

    protected bool startCo = true; // �ڷ�ƾ�� �����ߴ°�
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, ranginPlayer);
        Gizmos.DrawWireSphere(transform.position, ranginShot);
    }
    protected virtual void Init() // �ʱⰪ
    {
        if(anim == null)
            anim = GetComponent<Animator>();
        if (rigid == null)
            rigid = GetComponent<Rigidbody2D>();
        if (audio == null)
            audio = GetComponent<AudioSource>();
        if(capCol == null)
            capCol = GetComponent<CapsuleCollider2D>();
        capCol.enabled = true;
        curEnemyHp = enemyHp;
        curEnemySpeed = enemySpeed;
        curEnemyBulletSpeed = enemyBulletSpeed;
    }

    public void IsHit(int damage = 1)
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
    
    protected virtual void SetState(State newState) // ���¸� �������ִ� �Լ�
    {   
        curState = newState;
    }

    protected virtual void PlaySound(string action) // ���带 �����ϴ� �Լ�
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

    protected void OnCollisionStay2D(Collision2D collision) // ���� �÷��̾�� �������� ��� ������� �ִ� �Լ�
    {
        if (isDie == false)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<Character>().PlayerHIt();
                // ������ �÷��̾�� ������ ������� ��
            }
        }
    }
}
