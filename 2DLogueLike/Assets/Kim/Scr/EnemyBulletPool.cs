using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletPool : MonoBehaviour
{
    public Enemybullet prefab;
    public Transform parent;
    Queue<Enemybullet> pool = new Queue<Enemybullet>();

    public Enemybullet bossBullet;
    public Transform bossBulletParent;
    Queue<Enemybullet> bossBulletPool = new Queue<Enemybullet>();
    private void Awake()
    {
        for (int i = 0; i < 500; i++)
        {
            Enemybullet bullet = Instantiate(prefab);
            bullet.gameObject.SetActive(false);
            bullet.transform.SetParent(parent);
            pool.Enqueue(bullet);
        }
        /*for (int i = 0; i < 500; i++)
        {
            Enemybullet bullet = Instantiate(bossBullet);
            bullet.gameObject.SetActive(false);
            bullet.transform.SetParent(bossBulletParent);
            pool.Enqueue(bullet);
        }*/
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
    /*public Enemybullet GetBossBullet()
    {
        if (bossBulletPool.Count > 0)
        {
            Enemybullet bullet = bossBulletPool.Dequeue();
            bullet.gameObject.SetActive(true);
            return bullet;
        }
        else
        {
            Enemybullet bullet = Instantiate(bossBullet);
            bullet.gameObject.SetActive(true);
            bullet.transform.SetParent(bossBulletParent);
            bossBulletPool.Enqueue(bullet);
            return bullet;
        }
    }*/
    /*public void ReturnBossBullet(Enemybullet bullet)
    {
        bullet.gameObject.SetActive(false);
        bullet.transform.SetParent(bossBulletParent);
        bossBulletPool.Enqueue(bullet);
    }*/
    public void ReturnBullet(Enemybullet bullet)
    {
        bullet.gameObject.SetActive(false);
        bullet.transform.SetParent(parent);
        pool.Enqueue(bullet);
    }
}
