using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageStart : MonoBehaviour
{
    
    
    [SerializeField]
    Collider2D monsters; // ��� �ʿ� ���Ͱ� �����ϴ��� �˻��մϴ�.
    
    [SerializeField]
    GameObject wall; // ���� �ʿ� ���Ͱ� �����ϸ� �ݾ��� ��(��)���Դϴ�.

   
   
    private void FixedUpdate()
    {
        
        
        if(SceneManager.GetActiveScene().name == "BossRoom") // ���͵��� �����ϴ��� Ȯ���մϴ�.
        {
            monsters = Physics2D.OverlapBox(transform.position, GetComponent<BoxCollider2D>().size, 0, LayerMask.GetMask("Boss"));
        }
        else 
        {
            monsters = Physics2D.OverlapBox(transform.position, GetComponent<BoxCollider2D>().size, 0, LayerMask.GetMask("Enemy"));
        }



        //���� ���͵��� �ʿ� �ϳ��� �ִٸ� ��(��)�� ���ܼ� �÷��̾ �ٸ������� �̵����� ���ϵ��� �մϴ�.
        if (monsters != null) 
        {
            wall.SetActive(true);
            
        }
        else if(monsters == null)
        {
            
            wall.SetActive(false);
        }

        

    }


}
