using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] Player playerS;
    [SerializeField] Slider playerDurabilityBar;
    [SerializeField] Slider playerOilBar;

    [Header("Score")]
    [SerializeField] public float score;

    [Header("Boss")]
    [SerializeField] float bossWaitDelay;
    [SerializeField] GameObject boss;
    [SerializeField] Boss bossS;
    [SerializeField] Slider bossHPBar;


    private void Update()
    {
        setDurability();
        oilSet();
        BossHPSet();
        BossSet();
    }

    void setDurability()
    {
        playerDurabilityBar.value = playerS.hp / 30;
    }

    void oilSet()
    {
        playerOilBar.value = playerS.oil / 100;
    }

    void BossHPSet()
    {
        bossHPBar.value = bossS.hp / 1000;
    }

    void BossSet()
    {
        if(bossWaitDelay < 100)
        {
            bossWaitDelay += Time.deltaTime;
            return;
        }

        boss.gameObject.SetActive(true);
        bossHPBar.gameObject.SetActive(true);
    }
}
