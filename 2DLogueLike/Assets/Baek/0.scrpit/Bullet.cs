using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 200f;
    public Transform target;
    public bool isMove = true;
    Rigidbody2D rigid;
    Collider2D player;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        target = ObjectPoolBaek.Instance.Player.transform;
    }

    private void Update()
    {
        if(isMove == true)
        {
            Followshot();
        }
    }

    private void FixedUpdate()
    {
        player = Physics2D.OverlapCircle(transform.position, transform.localScale.x,LayerMask.GetMask("Player"));
        if (player != null)
        {
            player.GetComponent<Character>().PlayerHIt(1);
        }
        
    }

    void Followshot()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // 플레이어를 향해 회전
        Vector3 direction = target.position - transform.position;
        //Quaternion toRotation = Quaternion.LookRotation(direction);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationSpeed * Time.deltaTime);

        // 앞쪽으로 직진
        //transform.Translate(direction * speed * Time.deltaTime);
        rigid.velocity = direction * speed; // 노말라이즈 안한이유 오히려 멀어지면 더 피하기 힘들어 지게 하기위함.
        isMove = false;
    }
}
