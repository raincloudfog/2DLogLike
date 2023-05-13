using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : MonoBehaviour
{
    public static EnemyObjectPool instance;
    public Character player;
    
    public EnemyBulletPool enemyBulletpool;
    public EnemyPool enemyPool;
    public ItemPool itemPool;   

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
