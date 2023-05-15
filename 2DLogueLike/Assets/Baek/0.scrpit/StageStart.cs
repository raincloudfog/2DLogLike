using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageStart : MonoBehaviour
{
    
    
    [SerializeField]
    Collider2D monsters;
    
    [SerializeField]
    GameObject wall;

    [SerializeField]
    bool isSpon = false;

    private void Start()
    {
        
    }
   
    private void FixedUpdate()
    {
        
        
        if(SceneManager.GetActiveScene().name == "BossRoom")
        {
            monsters = Physics2D.OverlapBox(transform.position, GetComponent<BoxCollider2D>().size, 0, LayerMask.GetMask("Boss"));
        }
        else
        {
            monsters = Physics2D.OverlapBox(transform.position, GetComponent<BoxCollider2D>().size, 0, LayerMask.GetMask("Enemy"));
        }




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
