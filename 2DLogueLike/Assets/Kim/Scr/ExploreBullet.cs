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
            if (col.gameObject.layer == 6) // ���� ���̾ �÷��̾�� ������� ��
            {
                col.GetComponent<Character>().PlayerHIt();
            }
            else if(col.gameObject.layer == 8) // ���� ������ �����ϸ鼭 ���������� �ҷ��� �߻���
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
    // �Ѿ��� �Ӽ��� �����ش� (����, �ӵ�)
    {
        Init();
        bulletVec = offset.normalized * speed; // ���� ���Ͱ�
        rigid.velocity = bulletVec;
    }
}
