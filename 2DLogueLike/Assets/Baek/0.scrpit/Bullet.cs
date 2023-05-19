using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 200f;
    public Transform target;
    public bool isMove = true; // 더미 데이터
    Rigidbody2D rigid;
    Collider2D player;
    Collider2D Wall;
    
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        target = ObjectPoolBaek.Instance.Player.transform;
    }

    private void Update()
    {
        //Followshot();
        if (isMove == true) 
        {
            Followshot();
        }
    }

    private void FixedUpdate()
    {
        
        player = Physics2D.OverlapCircle(transform.position, transform.localScale.x,LayerMask.GetMask("Player"));
        Wall = Physics2D.OverlapCircle(transform.position, transform.localScale.x, LayerMask.GetMask("Wall"));
        if (player != null)
        {
            player.GetComponent<Character>().PlayerHIt(1);
        }
        if (Wall != null)
        {
            Destroy(gameObject);
        }

        
    }

    void Followshot()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // 플레이어를 향해 회전 // 이거는 유도탄입니다만 z스케일 값이 변경되어 사용하지 않게 되었습니다.
        Vector3 direction = target.position - transform.position;
        //Quaternion toRotation = Quaternion.LookRotation(direction);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationSpeed * Time.deltaTime);

        // 앞쪽으로 직진
        //transform.Translate(direction * speed * Time.deltaTime);
        rigid.velocity = direction * speed; // 노말라이즈 안한이유 오히려 멀어지면 더 피하기 힘들어 지게 하기위함.
        isMove = false;  // 원래 유도탄용도로 사용되었으나 총알이 빨라서 나두기로 했습니다. 
        // 지금 이코드에서 이것이 없으면 플레이어를 따라 총알이 계속 움직입니다. 진짜로 유도가 되어버려요. 사기적인 성능
    }
}
