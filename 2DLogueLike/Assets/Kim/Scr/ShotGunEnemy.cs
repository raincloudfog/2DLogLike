using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunEnemy : Enemy
{
    //Character player;
    //GameObject player;
    CapsuleCollider2D capCol;
    private void Awake()
    {
        Init();
        
    }
    private void OnEnable()
    {
        Init();
        capCol.enabled = true;
        curEnemyHp = enemyHp;
        curEnemySpeed = enemySpeed;
        curEnemyBulletSpeed = enemyBulletSpeed;
        curEnemyBulletDamage = enemybulletDamage;
        //player = EnemyObjectPool.instance.testPlayer; 캐싱을 하고싶지만 오류가 뜨기에 보류
    }
    protected override void Init()
    {
        base.Init();
        if(capCol == null)
        {
            capCol = GetComponent<CapsuleCollider2D>();
        }
    }


    void Update()
    {
        col = Physics2D.OverlapCircle(transform.position, ranginPlayer, playerLayer);
        
        switch (curState)
        {
            case State.Idle:
                Idle();
                break;

            case State.Attack:
                Attack();
                break;
        }
        
        Die();      
    }

    void Idle()
    {
        anim.SetBool("isMove", false);
        if (col != null && isDie == false)
        {
            SetState(State.Attack);
        }
    }

    void Attack()
    {
        Collider2D col2 = Physics2D.OverlapCircle(transform.position, ranginShot, playerLayer);
        anim.SetBool("isMove", col);
        if (col == null && isDie == false)
        {
            SetState(State.Idle);
            rigid.velocity = Vector2.zero;
        }
        else if (col != null && col2 == null && isDie == false)
        {
            //anim.SetBool("isMove", true);
            //Vector2 offset = player.transform.position - transform.position;
            Vector2 offset = EnemyObjectPool.instance.player.transform.position - transform.position;
            rigid.velocity = offset.normalized * curEnemySpeed;
            EnemyFilp();
        }


        else if (col2 != null && col != null && isDie == false)
        {
            anim.SetBool("isMove", false);
            EnemyFilp();
            rigid.velocity = Vector2.zero;
            if (isAttack)
            {
                isAttack = false;
                
                newShotgun();

                StartCoroutine(Delay());
            }
        }

    }
    void EnemyFilp()
    {
        Vector3 localScale = transform.localScale;
        //player.transform.position.x
        if (EnemyObjectPool.instance.player.transform.position.x < transform.position.x)
        {
            localScale.x = -1;
        }
        else
        {
            localScale.x = 1;
        }
        transform.localScale = localScale;
    }
 
    void newShotgun() //
    {
        Vector2 offset = EnemyObjectPool.instance.player.transform.position - transform.position;
        Vector2 dir;
        Enemybullet obj;
        for (int i = -1; i < 2; i++) //
        {
            /*if (i==0)
            {
                continue;
            }*/
            dir = Quaternion.AngleAxis( 10 * i, Vector3.forward) * offset;
            obj = EnemyObjectPool.instance.enemyBulletpool.Getbullet();
            obj.transform.position = transform.position;
            obj.SetRigidBullet(dir, curEnemyBulletSpeed, curEnemyBulletDamage);
        }
        PlaySound("isShot");
        
    }
    void Die()
    {
        if (isDie == true && isrealDie == false)
        {
            isrealDie = true;
            anim.SetTrigger("isDie");
            StopCoroutine(ReturnDelay());
            StartCoroutine(ReturnDelay());
            rigid.velocity = Vector2.zero;
            capCol.enabled = false;
        }
    }
    IEnumerator ReturnDelay()
    {
        yield return new WaitForSeconds(1f);
        EnemyObjectPool.instance.enemyPool.ReturnSgEnemy(this);
        int random = Random.Range(0, 2);
        switch (random)
        {
            case 0:
                Food food = EnemyObjectPool.instance.itemPool.GetFood();
                food.transform.position = transform.position;
                break;
            case 1:
                Coin coin = EnemyObjectPool.instance.itemPool.GetCoin();
                coin.transform.position = transform.position;
                break;
        }
        isrealDie = false;
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(shotDelay);
        isAttack = true;
    }
}
