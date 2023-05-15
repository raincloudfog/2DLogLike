using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : Npc
{

    private void Start()
    {
        _kind = NPCKind.Potion;
    }
}
