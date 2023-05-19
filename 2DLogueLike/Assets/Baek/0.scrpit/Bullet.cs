using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 200f;
    public Transform target;
    public bool isMove = true; // ���� ������
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

        // �÷��̾ ���� ȸ�� // �̰Ŵ� ����ź�Դϴٸ� z������ ���� ����Ǿ� ������� �ʰ� �Ǿ����ϴ�.
        Vector3 direction = target.position - transform.position;
        //Quaternion toRotation = Quaternion.LookRotation(direction);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationSpeed * Time.deltaTime);

        // �������� ����
        //transform.Translate(direction * speed * Time.deltaTime);
        rigid.velocity = direction * speed; // �븻������ �������� ������ �־����� �� ���ϱ� ����� ���� �ϱ�����.
        isMove = false;  // ���� ����ź�뵵�� ���Ǿ����� �Ѿ��� ���� ���α�� �߽��ϴ�. 
        // ���� ���ڵ忡�� �̰��� ������ �÷��̾ ���� �Ѿ��� ��� �����Դϴ�. ��¥�� ������ �Ǿ������. ������� ����
    }
}
