using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BossUI : MonoBehaviour
{
    public TMP_Text bossNameText;
    public Slider bossHpSlider;
    public Boss boss;
    bool isHpUpEnd = false;
    private void OnEnable()
    {
        bossNameText.text = "The Abyss Wizard";
        bossHpSlider.maxValue = boss.bossHp;
        //StartCoroutine(Hpup());
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isHpUpEnd == false)
        {
            bossHpSlider.value += 1f;
            if (bossHpSlider.value == bossHpSlider.maxValue)
            {
                isHpUpEnd = true;
            }
        }
        else if (isHpUpEnd == true)
        {
            bossHpSlider.value = boss.bossHp;
        }
    }
    IEnumerator Hpup()
    {
        while (isHpUpEnd == false)
        {
            bossHpSlider.value += 1.5f;
            if (bossHpSlider.value == bossHpSlider.maxValue)
            {
                isHpUpEnd = true;
            }
        }
        yield return null;
    }
}
