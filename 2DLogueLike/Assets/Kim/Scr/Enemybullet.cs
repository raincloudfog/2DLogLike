using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemybullet : MonoBehaviour
{
    Rigidbody2D rigid;
    CircleCollider2D circleCol;
    Collider2D col = new Collider2D();
    int bulletDamage = 0;
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
                col.GetComponent<Character>().PlayerHIt(bulletDamage);
            }
            EnemyObjectPool.instance.enemyBulletpool.ReturnBullet(this);
        }
    }
    public void SetRigidBullet(Vector2 offset,float speed,int bulletDamage) 
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
    public void RigidBulletAgain() 
    // �Ͻ������� Ǯ���� ��� ���δϱ� �����س��� �ҷ����͸� �ҷ��´�.           
    {
        if(rigid == null)
        {
            rigid = this.GetComponent<Rigidbody2D>();
        }
        rigid.velocity = bulletVec; // ������ ���� �ҷ����� �ҷ�����
    }
    public void Stop()
    {
        rigid.velocity = Vector2.zero;
    }



    /*IEnumerator Disable() // �Ѿ��� �߻�� 3�� �� ���Ͻ�Ű�� �ڷ�ƾ ���̵�����

    {
        yield return new WaitForSeconds(3f);
        EnemyObjectPool.instance.enemyBulletpool.ReturnBullet(this);
    }*/

}
