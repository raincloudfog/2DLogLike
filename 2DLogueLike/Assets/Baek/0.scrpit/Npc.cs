using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public int Hp;
    public float speed;
    public Collider2D player;
    [SerializeField] protected float ranginPlayer; // �÷��̾ �߰��ϴ� ����
     
    public bool istalking = true;
    [SerializeField] GameObject UI;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, ranginPlayer);
        
    }

    private void Update()
    {
        if(player != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                UI.SetActive(true);
                GameManager.Instance.Player.isAttack = false;
            }
        }
        else
        {
            UI.SetActive(false);
            GameManager.Instance.Player.isAttack = true;
        }
    }
    private void FixedUpdate()
    {
        player = Physics2D.OverlapCircle(transform.position, transform.localScale.x * 2, LayerMask.GetMask("Player"));

        if(player != null)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }

    }
    public virtual void Talk()
    {

    }
}
