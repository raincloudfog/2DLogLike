using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageStart : MonoBehaviour
{
    
    
    [SerializeField]
    Collider2D monsters; // 모든 맵에 몬스터가 존재하는지 검사합니다.
    
    [SerializeField]
    GameObject wall; // 만약 맵에 몬스터가 존재하면 닫아줄 문(벽)들입니다.

   
   
    private void FixedUpdate()
    {
        
        
        if(SceneManager.GetActiveScene().name == "BossRoom") // 몬스터들이 존재하는지 확인합니다.
        {
            monsters = Physics2D.OverlapBox(transform.position, GetComponent<BoxCollider2D>().size, 0, LayerMask.GetMask("Boss"));
        }
        else 
        {
            monsters = Physics2D.OverlapBox(transform.position, GetComponent<BoxCollider2D>().size, 0, LayerMask.GetMask("Enemy"));
        }



        //만약 몬스터들이 맵에 하나라도 있다면 벽(문)이 생겨서 플레이어가 다른곳으로 이동하지 못하도록 합니다.
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
