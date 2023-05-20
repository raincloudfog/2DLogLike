using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemybullet : MonoBehaviour
{
    Rigidbody2D rigid;
    CircleCollider2D circleCol;
    Collider2D col = new Collider2D();
    Vector2 bulletVec = Vector2.zero;
    void Init()
    {
        if(rigid == null)
            rigid = this.GetComponent<Rigidbody2D>();
        if(circleCol == null)
            circleCol = this.GetComponent<CircleCollider2D>();
    }
    private void FixedUpdate()
    {
        col = Physics2D.OverlapCircle(transform.position, transform.localScale.x, 
            LayerMask.GetMask("Wall","Player"));
        if (col != null)
        {
            if (col.gameObject.layer == 6)
            {
                col.GetComponent<Character>().PlayerHIt();
            }
            EnemyObjectPool.instance.enemyBulletpool.ReturnBullet(this);
        }
    }
    public void SetRigidBullet(Vector2 offset,float speed) 
        // �Ѿ��� �Ӽ��� �����ش� (����, �ӵ�)
    {
        Init();
        bulletVec = offset.normalized * speed; // ���� ���Ͱ�
        rigid.velocity = bulletVec;
    }
}
