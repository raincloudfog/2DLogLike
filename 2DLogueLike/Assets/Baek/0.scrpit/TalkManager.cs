using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : SingletonBaek<TalkManager>
{
    [SerializeField] Npc npc;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

    }
    private void Update()
    {
        
    }

    void Talk()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(npc.player != null)
            {

            }
        }
    }
}
