using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : MonoBehaviour
{
    //적들의 오브젝트 풀 및 많이 불러와야 할 정보를 담당하는 싱글톤

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
