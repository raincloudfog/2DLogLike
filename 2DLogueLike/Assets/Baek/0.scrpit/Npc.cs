using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCKind
{
    Weapon,
    Potion,

    End
}

public class Npc : MonoBehaviour
{      
    public NPCKind _kind; //���� � NPC���� �˷��ֵ���..
    public int Hp;
    public float speed;
    public Collider2D player;
    [SerializeField] protected float ranginPlayer; // �÷��̾ �߰��ϴ� ����
     
    public bool istalking = true;
    [SerializeField] protected GameObject UI;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, ranginPlayer);
        
    }

    public void EventUI(bool isbool) // UI����
    {
        UI.SetActive(isbool); 
        GameManager.Instance.isUiactive = isbool; 
    }
   
    private void FixedUpdate()
    {
        player = Physics2D.OverlapCircle(transform.position, transform.localScale.x * 2, LayerMask.GetMask("Player")); // �÷��̾ �����ϸ� ���� üũ�ڽ� ǥ��

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
