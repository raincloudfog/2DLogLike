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
    public NPCKind _kind; //내가 어떤 NPC인지 알려주도록..
    public int Hp;
    public float speed;
    public Collider2D player;
    [SerializeField] protected float ranginPlayer; // 플레이어를 발견하는 범위
     
    public bool istalking = true;
    [SerializeField] protected GameObject UI;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, ranginPlayer);
        
    }

    public void EventUI(bool isbool) // UI실행
    {
        UI.SetActive(isbool); 
        GameManager.Instance.isUiactive = isbool; 
    }
   
    private void FixedUpdate()
    {
        player = Physics2D.OverlapCircle(transform.position, transform.localScale.x * 2, LayerMask.GetMask("Player")); // 플레이어를 감지하면 위의 체크박스 표시

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
