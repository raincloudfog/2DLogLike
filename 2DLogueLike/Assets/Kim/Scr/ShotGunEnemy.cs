using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunEnemy : Enemy
{
    Collider2D col2 = new Collider2D();
    private void Awake()
    {
        Init();
    }
    private void OnEnable()
    {
        Init();
    }
    void Update()
    {
        col = Physics2D.OverlapCircle(transform.position, ranginPlayer, playerLayer);
        offset = EnemyObjectPool.instance.player.transform.position - transform.position;
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
        col2 = Physics2D.OverlapCircle(transform.position, ranginShot, playerLayer);
        anim.SetBool("isMove", col);
        if (col == null && isDie == false)
        {
            SetState(State.Idle);
            rigid.velocity = Vector2.zero;
        }
        else if (col != null && col2 == null && isDie == false)
        {
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
        localScale = transform.localScale;
        localScale.x = EnemyObjectPool.instance.player.transform.position.x < transform.position.x ? -1 : 1;
        transform.localScale = localScale;
    }
 
    void newShotgun() //
    { 
        Vector2 dir;
        Enemybullet obj;
        for (int i = -1; i < 2; i++) //
        {
            dir = Quaternion.AngleAxis( 10 * i, Vector3.forward) * offset;
            obj = EnemyObjectPool.instance.enemyBulletpool.Getbullet();
            obj.transform.position = transform.position;
            obj.SetRigidBullet(dir, curEnemyBulletSpeed);
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
