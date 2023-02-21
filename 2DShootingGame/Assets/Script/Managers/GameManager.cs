using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("점수관련")]
    public int score;
    [SerializeField] Text scoreText;

    [Header("플레이어 관련")]
    [SerializeField] Player playerS;
    float maxPlayerHP;
    [SerializeField] Slider playerHPBar;
    [SerializeField] Text playerHPText;

    [Header("고통 게이지")]
    [SerializeField] public float curSick;
    [SerializeField] float maxSick;
    [SerializeField] Slider sickBar;
    [SerializeField] Text sickText;

    [Header("보스 관련")]
    [SerializeField] GameObject bossObject;
    [SerializeField] float bossTimer;


    private void Awake()
    {
        maxSick = 100;
        maxPlayerHP = playerS.hp;
    }

    private void Update()
    {
        SetScore();
        SetHP();
        SetSick();
        BossTime();
    }

    void SetScore()
    {
        scoreText.text = "Score : " + score;
    }

    void SetHP()
    {
        playerHPText.text = "PlayerHP : " + playerS.hp;
        playerHPBar.value = playerS.hp / maxPlayerHP;

        if(playerS.hp > 30)
        {
            playerS.hp = 30;
        }
    }

    void SetSick()
    {
        sickBar.value = curSick / maxSick;
        sickText.text = "현재 고통 : " + curSick;

        if(curSick < 0)
        {
            curSick = 0;
        }
    }

    void BossTime()
    {
        if(bossTimer < 80)
        {
            bossObject.SetActive(false);
        }
    }
}
