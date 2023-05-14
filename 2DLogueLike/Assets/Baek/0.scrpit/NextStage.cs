using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStage : MonoBehaviour
{
    Collider2D Player;

    private void FixedUpdate()
    {
        Player = Physics2D.OverlapBox(transform.position, GetComponent<BoxCollider2D>().size,0,LayerMask.GetMask("Player"));
        if(Player != null)
        {
            SceneManager.LoadScene(2);
        }
    }
}
