using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    public Food foodPrefab;
    public Transform foodParent;
    Queue<Food> foodPool = new Queue<Food>();

    public Coin coinPrefab;
    public Transform coinParent;
    Queue<Coin> coinPool = new Queue<Coin>();

    private void Awake()
    {
        for (int i = 0; i < 20; i++)
        {
            Food foodobj = Instantiate(foodPrefab); 
            foodobj.gameObject.SetActive(false);
            foodobj.transform.SetParent(foodParent);
            foodPool.Enqueue(foodobj);

            Coin coinobj = Instantiate(coinPrefab);
            coinobj.gameObject.SetActive(false);
            coinobj.transform.SetParent(coinParent);
            coinPool.Enqueue(coinobj);
        }
    }

    public Food GetFood()
    {
        if (foodPool.Count > 0)
        {
            Food foodobj = foodPool.Dequeue();
            foodobj.gameObject.SetActive(true);
            return foodobj;
        }
        else
        {
            Food foodobj = Instantiate(foodPrefab);
            foodobj.gameObject.SetActive(true);
            foodobj.transform.SetParent(foodParent);
            foodPool.Enqueue(foodPrefab);
            return foodobj;
        }
    }
    public Coin GetCoin()
    {
        if (coinPool.Count > 0)
        {
            Coin coinobj = coinPool.Dequeue();
            coinobj.gameObject.SetActive(true);
            return coinobj;
        }
        else
        {
            Coin coinobj = Instantiate(coinPrefab);
            coinobj.gameObject.SetActive(true);
            coinobj.transform.SetParent(coinParent);
            coinPool.Enqueue(coinobj);
            return coinobj;
        }
    }
    public void ReturnCoin(Coin coinobj)
    {
        coinobj.gameObject.SetActive(false);
        coinobj.transform.SetParent(coinParent);
        coinPool.Enqueue(coinobj);
    }
    public void ReturnFood(Food foodobj)
    {
        foodobj.gameObject.SetActive(false);
        foodobj.transform.SetParent(foodParent);
        foodPool.Enqueue(foodobj);
    }
}
