using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : Npc
{
    public GameObject Shop; // 무기 상인을 감지하기 위함
    private void Start()
    {
        _kind = NPCKind.Potion;
    }
    private void FixedUpdate()
    {
        if(Shop.activeSelf == false) // 만약 상인이 죽을경우 
        {
            gameObject.SetActive(false); // 간호사는 사라집니다.
        }
    }
}
