using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    // 적들을 담고 있는 풀
    
    // 단발을 쏘는 적 
    public PistorEnemy ptPrefab;
    public Transform ptParent;
    Queue<PistorEnemy> ptPool = new Queue<PistorEnemy>();

    // 3발씩 쏘는 적
    public ShotGunEnemy sgPrefab;
    public Transform sgParent;
    Queue<ShotGunEnemy> sgPool = new Queue<ShotGunEnemy>();

    // 플레이어를 쫒아가는 적
    public RushEnemy rPrefab;
    public Transform rParent;
    Queue<RushEnemy> rPool = new Queue<RushEnemy>();

    // 플레이어 발견시 돌진하는 적
    public FlyEnemy fPrefab;
    public Transform fParent;
    Queue<FlyEnemy> fPool = new Queue<FlyEnemy>();

    private void Awake()
    {
        for (int i = 0; i < 20; i++)
        {
            // 현재는 적들을 위치를 직접 정해주고 배치해뒀기 떄문에
            // 주석처리함 -> 필요할시 사용

            /*PistorEnemy ptEnemy = Instantiate(ptPrefab);
            ptEnemy.gameObject.SetActive(false);
            ptEnemy.transform.SetParent(ptParent);
            ptPool.Enqueue(ptEnemy);

            ShotGunEnemy sgEnemy = Instantiate(sgPrefab);
            sgEnemy.gameObject.SetActive(false);
            sgEnemy.transform.SetParent(sgParent);
            sgPool.Enqueue(sgEnemy);

            RushEnemy rEnemy = Instantiate(rPrefab);
            rEnemy.gameObject.SetActive(false);
            rEnemy.transform.SetParent(rParent);
            rPool.Enqueue(rEnemy);*/
        }
    }
    public PistorEnemy GetPtEnemy()
    {
        if (ptPool.Count > 0)
        {
            PistorEnemy ptEnemy = ptPool.Dequeue();
            ptEnemy.gameObject.SetActive(true);
            return ptEnemy;
        }
        else
        {
            PistorEnemy ptEnemy = Instantiate(ptPrefab);
            ptEnemy.gameObject.SetActive(true);
            ptEnemy.transform.SetParent(ptParent);
            ptPool.Enqueue(ptEnemy);
            return ptEnemy;
        }
    }

    public ShotGunEnemy GetSgEnemy()
    {
        if (sgPool.Count > 0)
        {
            ShotGunEnemy sgEnemy = sgPool.Dequeue();
            sgEnemy.gameObject.SetActive(true);
            return sgEnemy;
        }
        else
        {
            ShotGunEnemy sgEnemy = Instantiate(sgPrefab);
            sgEnemy.gameObject.SetActive(true);
            sgEnemy.transform.SetParent(sgParent);
            sgPool.Enqueue(sgEnemy);
            return sgEnemy;
        }
    }

    public RushEnemy GetREnemy()
    {
        if (rPool.Count > 0)
        {
            RushEnemy rEnemy = rPool.Dequeue();
            rEnemy.gameObject.SetActive(true);
            return rEnemy;
        }
        else
        {
            RushEnemy rEnemy = Instantiate(rPrefab);
            rEnemy.gameObject.SetActive(true);
            rEnemy.transform.SetParent(rParent);
            rPool.Enqueue(rEnemy);
            return rEnemy;
        }
    }

    public FlyEnemy GetFEnemy()
    {
        if (fPool.Count > 0)
        {
            FlyEnemy fEnemy = fPool.Dequeue();
            fEnemy.gameObject.SetActive(true);
            return fEnemy;
        }
        else
        {
            FlyEnemy fEnemy = Instantiate(fPrefab);
            fEnemy.gameObject.SetActive(true);
            fEnemy.transform.SetParent(rParent);
            fPool.Enqueue(fEnemy);
            return fEnemy;
        }
    }
    public void ReturnPtEnemy(PistorEnemy ptEnemy)
    {
        ptPool.Enqueue(ptEnemy);
        ptEnemy.gameObject.SetActive(false);
        ptEnemy.transform.SetParent(ptParent);
    }
    public void ReturnSgEnemy(ShotGunEnemy sgEnemy)
    {
        sgEnemy.gameObject.SetActive(false);
        sgEnemy.transform.SetParent(sgParent);
        sgPool.Enqueue(sgEnemy);
    }
    public void ReturnREnemy(RushEnemy rEnemy)
    {
        rEnemy.gameObject.SetActive(false);
        rEnemy.transform.SetParent(rParent);
        rPool.Enqueue(rEnemy);
    }
    public void ReturnFEnemy(FlyEnemy fEnemy)
    {
        fEnemy.gameObject.SetActive(false);
        fEnemy.transform.SetParent(rParent);
        fPool.Enqueue(fEnemy);
    }
}
