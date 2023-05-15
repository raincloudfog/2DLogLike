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

    private void Update()
    {
        /*if(player != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("isUiactive = true");
                //GameManager.Instance.isUiactive = true;
                GameManager.Instance.SetIsTalking(true, _kind);
                UI.SetActive(true);                
            }            
        }
        else
        {
            Debug.Log("isUiactive = false;");
            GameManager.Instance.SetIsTalking(false, _kind);
            //GameManager.Instance.isUiactive = false;
            UI.SetActive(false);            
        }*/
        /*if (UI.activeSelf == true)
        {
            GameManager.Instance.Player.isAttack = false;
        }
        else if (UI.activeSelf == false)
        {
            GameManager.Instance.Player.isAttack = true;
        }
*/


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
