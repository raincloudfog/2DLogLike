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
        // col�� ���������� �ҷ��� ���̱⶧���� ���ý����� x�� ����
        if (col != null) // ���� ������ ���̾ ���� ������Ʈ�� ��Ҵٸ�
        {
            if (col.gameObject.layer == 6) // Player ���̾��� ���
            {
                col.GetComponent<Character>().PlayerHIt(bulletDamage); // ĳ������ PlayerHit�� �����Ͽ� ������� ����
            }
            EnemyObjectPool.instance.enemyBulletpool.ReturnBossBullet(this);
            // �̱��濡 �ִ� enemyBulletpool�� �ִ� �����Լ��� �ǵ����ش�;
        }
    }
    public void SetRigidBossBullet(Vector2 offset, float speed, int bulletDamage)
    // �Ѿ��� �Ӽ��� �����ش� (����, �ӵ�, �����)
    {
        Init();
        if (this.bulletDamage == 0)
        {
            this.bulletDamage = bulletDamage;
        }
        bulletVec = offset.normalized * speed; // ���� ���Ͱ�
        rigid.velocity = bulletVec;

    }

    /*public void RigidBossBulletAgain()  // �ʿ���� �Լ�
    // �Ͻ������� Ǯ���� ��� ���δϱ� �����س��� �ҷ����͸� �ҷ��´�.           
    {
        if (rigid == null)
        {
            rigid = this.GetComponent<Rigidbody2D>();
        }
        rigid.velocity = bulletVec; // ������ ���� �ҷ����� �ҷ�����
    }
    public void Stop()
    {
        rigid.velocity = Vector2.zero;
    }*/
}
