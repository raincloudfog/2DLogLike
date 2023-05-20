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
        // col�� ���������� �ҷ��� ���̱⶧���� ���ý����� x�� ����
        if (col != null) // ���� ������ ���̾ ���� ������Ʈ�� ��Ҵٸ�
        {
            if (col.gameObject.layer == 6) // Player ���̾��� ���
            {
                col.GetComponent<Character>().PlayerHIt(); // ĳ������ PlayerHit�� �����Ͽ� ������� ����
            }
            EnemyObjectPool.instance.enemyBulletpool.ReturnBossBullet(this);
            // �̱��濡 �ִ� enemyBulletpool�� �ִ� �����Լ��� �ǵ����ش�;
        }
    }
    public void SetRigidBossBullet(Vector2 offset, float speed)
    // �Ѿ��� �Ӽ��� �����ش� (����, �ӵ�)
    {
        Init();
        bulletVec = offset.normalized * speed; // ���� ���Ͱ�
        rigid.velocity = bulletVec;
    }
}
