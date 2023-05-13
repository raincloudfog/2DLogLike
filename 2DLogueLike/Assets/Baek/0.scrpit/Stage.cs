using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField]
    Collider2D player;

    [SerializeField]
    List<GameObject> monstersList;

    private void Awake()
    {
        for (int i = 0; i < monstersList.Count; i++)
        {

            monstersList[i].SetActive(false);

        }
    }

    private void FixedUpdate()
    {
        player = Physics2D.OverlapBox(transform.position, GetComponent<BoxCollider2D>().size, 0,LayerMask.GetMask("Player"));
        if(player != null)
        {
            
            for (int i = 0; i < monstersList.Count; i++)
            {
                monstersList[i].gameObject.SetActive(true);
            }
                
            
            gameObject.SetActive(false);

        }
    }
}
