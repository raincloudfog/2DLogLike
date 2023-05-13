using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gundrop : Item
{
    Collider2D Pick;
    Character Player;
    [SerializeField] Sprite[] Guns;
    [SerializeField] Sprite mainspr;
    int rand;
    public void Init()
    {
        
        if (Player == null)
            Player = ObjectPoolBaek.Instance.Player;
        if (GetComponent<SpriteRenderer>().sprite == mainspr)
        {
            rand = Random.Range(0, 5);
            if ((int)GunManager.Instance.waeponType == rand)
                rand = Random.Range(0, 5);
            GetComponent<SpriteRenderer>().sprite = Guns[rand];
        }
            
        //Debug.Log(rand);
    }
    public override void Pick_Up()
    {
        Init();

        if (Pick != null)
        {
            
            Debug.Log("플레이어 닿임");
            GunManager.Instance.waeponType = (WaeponType)rand;
            Destroy(gameObject);

        }
    }
    private void FixedUpdate()
    {
        Pick = Physics2D.OverlapCircle(transform.position,
               transform.localPosition.x / 2,
               LayerMask.GetMask("Player"));
        Pick_Up();
    }
}
