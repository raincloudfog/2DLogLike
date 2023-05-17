using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : Npc
{
    public GameObject Shop;
    private void Start()
    {
        _kind = NPCKind.Potion;
    }
    private void FixedUpdate()
    {
        if(Shop.activeSelf == false)
        {
            gameObject.SetActive(false);
        }
    }
}
