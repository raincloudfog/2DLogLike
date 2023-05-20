using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploreBullet : MonoBehaviour
{
    Rigidbody2D rigid;
    CircleCollider2D circleCol;
    Collider2D col = new Collider2D();
    
    Vector2 bulletVec = Vector2.zero;
    void Init()
    {
        if (rigid == null)
            rigid = this.GetComponent<Rigidbody2D>();
        if (circleCol == null)
            circleCol = this.GetComponent<CircleCollider2D>();
    }
    private void Update()
    {
        transform.rotation *= Quaternion.Euler(Vector3.forward * -5f);
    }
    private void FixedUpdate()
    {
        col = Physics2D.OverlapCircle(transform.position, transform.localScale.x,
            LayerMask.GetMask("Wall", "Player"));
        if (col != null) 
        {
            if (col.gameObject.layer == 6) // 닿은 레이어가 플레이어면 대미지를 줌
            {
                col.GetComponent<Character>().PlayerHIt();
            }
            else if(col.gameObject.layer == 8) // 벽에 닿으면 폭발하면서 전방향으로 불렛을 발사함
            {
                for (int i = 0; i < 30; ++i)
                {
                    BossBullet bullet = EnemyObjectPool.instance.enemyBulletpool.GetBossBullet();
                    float angle = (360 / 30) * i;
                    float x = Mathf.Cos(angle * Mathf.PI / 180.0f);
                    float y = Mathf.Sin(angle * Mathf.PI / 180.0f);
                    Vector2 dir = new Vector2(x, y);
                    bullet.transform.position = transform.position;
                    bullet.SetRigidBossBullet(dir, 6);
                }
                EnemyObjectPool.instance.enemyBulletpool.ReturnExploreBullet(this);
            }
        }
    }
    public void SetRigidExploreBullet(Vector2 offset, float speed)
    // 총알의 속성을 정해준다 (방향, 속도)
    {
        Init();
        bulletVec = offset.normalized * speed; // 받은 벡터값
        rigid.velocity = bulletVec;
    }
}
