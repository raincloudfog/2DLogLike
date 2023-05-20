using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
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
    private void FixedUpdate()
    {
        col = Physics2D.OverlapCircle(transform.position, transform.localScale.x,
            LayerMask.GetMask("Wall", "Player"));
        // col의 감지범위는 불렛이 원이기때문에 로컬스케일 x로 설정
        if (col != null) // 만약 설정한 레이어를 가진 오브젝트가 닿았다면
        {
            if (col.gameObject.layer == 6) // Player 레이어일 경우
            {
                col.GetComponent<Character>().PlayerHIt(); // 캐릭터의 PlayerHit에 접근하여 대미지를 입힘
            }
            EnemyObjectPool.instance.enemyBulletpool.ReturnBossBullet(this);
            // 싱글톤에 있는 enemyBulletpool에 있는 리턴함수로 되돌려준다;
        }
    }
    public void SetRigidBossBullet(Vector2 offset, float speed)
    // 총알의 속성을 정해준다 (방향, 속도)
    {
        Init();
        bulletVec = offset.normalized * speed; // 받은 벡터값
        rigid.velocity = bulletVec;
    }
}
