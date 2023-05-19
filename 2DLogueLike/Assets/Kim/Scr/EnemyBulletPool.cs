using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletPool : MonoBehaviour
{ 
    //ÀûµéÀÇ ºÒ·¿µéÀ» ´ã´Â Ç®

    // Àâ¸÷ÀÇ ºÒ·¿
    public Enemybullet prefab;
    public Transform parent;
    Queue<Enemybullet> pool = new Queue<Enemybullet>();

    // º¸½ºÀÇ ºÒ·¿
    public BossBullet bossBullet;
    public Transform bossBulletParent;
    Queue<BossBullet> bossBulletPool = new Queue<BossBullet>();

    // º¸½ºÀÇ Æø¹ß ºÒ·¿
    public ExploreBullet exploreBullet;
    public Transform exploreBulletParent;
    Queue<ExploreBullet> exploreBulletPool = new Queue<ExploreBullet>();
    private void Awake()
    {
        for (int i = 0; i < 500; i++)
        {
            Enemybullet bullet = Instantiate(prefab);
            bullet.gameObject.SetActive(false);
            bullet.transform.SetParent(parent);
            pool.Enqueue(bullet);
        }
        for (int i = 0; i < 500; i++)
        {
            BossBullet bullet = Instantiate(bossBullet);
            bullet.gameObject.SetActive(false);
            bullet.transform.SetParent(bossBulletParent);
            bossBulletPool.Enqueue(bullet);
        }
        for (int i = 0; i < 5; i++)
        {
            ExploreBullet bullet = Instantiate(exploreBullet);
            bullet.gameObject.SetActive(false);
            bullet.transform.SetParent(exploreBulletParent);
            exploreBulletPool.Enqueue(bullet);
        }
    }

    public Enemybullet Getbullet()
    {
        if(pool.Count > 0)
        {
            Enemybullet bullet = pool.Dequeue();
            bullet.gameObject.SetActive(true);
            return bullet;
        }
        else
        {
            Enemybullet bullet = Instantiate(prefab);
            bullet.gameObject.SetActive(true);
            bullet.transform.SetParent(parent);
            pool.Enqueue(bullet);
            return bullet;
        }
    }
    public BossBullet GetBossBullet()
    {
        if (bossBulletPool.Count > 0)
        {
            BossBullet bullet = bossBulletPool.Dequeue();
            bullet.gameObject.SetActive(true);
            return bullet;
        }
        else
        {
            BossBullet bullet = Instantiate(bossBullet);
            bullet.gameObject.SetActive(true);
            bullet.transform.SetParent(bossBulletParent);
            bossBulletPool.Enqueue(bullet);
            return bullet;
        }
    }

    public ExploreBullet GetExploreBullet()
    {
        if (exploreBulletPool.Count > 0)
        {
            ExploreBullet bullet = exploreBulletPool.Dequeue();
            bullet.gameObject.SetActive(true);
            return bullet;
        }
        else
        {
            ExploreBullet bullet = Instantiate(exploreBullet);
            bullet.gameObject.SetActive(true);
            bullet.transform.SetParent(exploreBulletParent);
            exploreBulletPool.Enqueue(bullet);
            return bullet;
        }
    }

    public void ReturnBossBullet(BossBullet bullet)
    {
        bullet.gameObject.SetActive(false);
        bullet.transform.SetParent(bossBulletParent);
        bossBulletPool.Enqueue(bullet);
    }
    public void ReturnBullet(Enemybullet bullet)
    {
        bullet.gameObject.SetActive(false);
        bullet.transform.SetParent(parent);
        pool.Enqueue(bullet);
    }

    public void ReturnExploreBullet(ExploreBullet bullet)
    {
        bullet.gameObject.SetActive(false);
        bullet.transform.SetParent(exploreBulletParent);
        exploreBulletPool.Enqueue(bullet);
    }
}
