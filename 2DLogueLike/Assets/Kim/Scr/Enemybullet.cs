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
    public void RigidBulletAgain() 
    // 일시정지를 풀었을 경우 제로니까 저장해놓은 불렛벡터를 불러온다.           
    {
        if(rigid == null)
        {
            rigid = this.GetComponent<Rigidbody2D>();
        }
        rigid.velocity = bulletVec; // 저장해 놓은 불렛백터 불러오기
    }
    public void Stop()
    {
        rigid.velocity = Vector2.zero;
    }



    /*IEnumerator Disable() // 총알이 발사된 3초 후 리턴시키는 코루틴 더미데이터

    {
        yield return new WaitForSeconds(3f);
        EnemyObjectPool.instance.enemyBulletpool.ReturnBullet(this);
    }*/

}
