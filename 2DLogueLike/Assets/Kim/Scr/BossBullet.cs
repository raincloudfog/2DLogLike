using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    Rigidbody2D rigid;
    CircleCollider2D circleCol;
    Collider2D col = new Collider2D();
    int bulletDamage = 0;
    Vector2 bulletVec = Vector2.zero;
    void Init()
    {
        if (rigid == null)
            rigid = this.GetComponent<Rigidbody2D>();
        if (circleCol == null)
            circleCol = this.GetComponent<CircleCollider2D>();
    }
    private void FixedUpdate()
    {
        col = Physics2D.OverlapCircle(transform.position, transform.localScale.x,
            LayerMask.GetMask("Wall", "Player"));
        // col의 감지범위는 불렛이 원이기때문에 로컬스케일 x로 설정
        if (col != null) // 만약 설정한 레이어를 가진 오브젝트가 닿았다면
        {
            if (col.gameObject.layer == 6) // Player 레이어일 경우
            {
                col.GetComponent<Character>().PlayerHIt(bulletDamage); // 캐릭터의 PlayerHit에 접근하여 대미지를 입힘
            }
            EnemyObjectPool.instance.enemyBulletpool.ReturnBossBullet(this);
            // 싱글톤에 있는 enemyBulletpool에 있는 리턴함수로 되돌려준다;
        }
    }
    public void SetRigidBossBullet(Vector2 offset, float speed, int bulletDamage)
    // 총알의 속성을 정해준다 (방향, 속도, 대미지)
    {
        Init();
        if (this.bulletDamage == 0)
        {
            this.bulletDamage = bulletDamage;
        }
        bulletVec = offset.normalized * speed; // 받은 벡터값
        rigid.velocity = bulletVec;

    }

    /*public void RigidBossBulletAgain()  // 필요없는 함수
    // 일시정지를 풀었을 경우 제로니까 저장해놓은 불렛벡터를 불러온다.           
    {
        if (rigid == null)
        {
            rigid = this.GetComponent<Rigidbody2D>();
        }
        rigid.velocity = bulletVec; // 저장해 놓은 불렛백터 불러오기
    }
    public void Stop()
    {
        rigid.velocity = Vector2.zero;
    }*/
}
