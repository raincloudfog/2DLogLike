using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploreBullet : MonoBehaviour
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
    private void Update()
    {
        transform.rotation *= Quaternion.Euler(Vector3.forward * -0.5f);
    }
    private void FixedUpdate()
    {
        col = Physics2D.OverlapCircle(transform.position, transform.localScale.x,
            LayerMask.GetMask("Wall", "Player"));
        if (col != null)
        {
            if (col.gameObject.layer == 6)
            {
                col.GetComponent<Character>().PlayerHIt(bulletDamage);
            }
            else if(col.gameObject.layer == 8)
            {
                for (int i = 0; i < 30; ++i)
                {
                    BossBullet bullet = EnemyObjectPool.instance.enemyBulletpool.GetBossBullet();
                    float angle = (360 / 30) * i;
                    float x = Mathf.Cos(angle * Mathf.PI / 180.0f);
                    float y = Mathf.Sin(angle * Mathf.PI / 180.0f);
                    Vector2 dir = new Vector2(x, y);
                    bullet.transform.position = transform.position;
                    bullet.SetRigidBossBullet(dir, 6, bulletDamage);
                }
            }
            Destroy(this.gameObject);
        }
    }
    public void SetRigidExploreBullet(Vector2 offset, float speed, int bulletDamage)
    // �Ѿ��� �Ӽ��� �����ش� (����, �ӵ�, �����)
    {
        Init();
        if (this.bulletDamage == 0)
        {
            this.bulletDamage = bulletDamage;
        }
        bulletVec = offset.normalized * speed; // ���� ���Ͱ�
        rigid.velocity = bulletVec;
        //StartCoroutine(Disable());
    }
    public void RigidBossBulletAgain()
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
    }



    /*IEnumerator Disable() // �Ѿ��� �߻�� 3�� �� ���Ͻ�Ű�� �ڷ�ƾ 
    {
        yield return new WaitForSeconds(3f);
        
    }*/
}
