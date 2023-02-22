using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField] public float bossTimer;

    [Header("스테이지 관련")]
    [SerializeField] GameObject fadeOut;
    [SerializeField] Text stageText;
    [SerializeField] int stageNum;


    private void Awake()
    {
        Invoke("FadeOutOn", 0.5f);
        Invoke("FadeOutOff", 0.8f);
        stageNum = 1;
        StageStart();
        maxSick = 100;
        maxPlayerHP = playerS.hp;
    }

    private void Update()
    {
        SetScore();
        SetHP();
        SetSick();
        BossTime();

        if(curSick >= 100)
        {
            GameOver();
        }
    }


    void StageStart()
    {
        stageText.text = "Stage " + stageNum + "\nStart!";
        stageText.gameObject.SetActive(true);

        playerS.power = 1;
        playerS.hp = 30;

        if(stageNum == 1)
        {
            curSick = 10;
        }
        else if(stageNum == 2)
        {
            curSick = 20;
        }
        Invoke("StageTextOff", 1);
    }

    void StageTextOff()
    {
        stageText.gameObject.SetActive(false);
    }

    public void StageEnd()
    {
        stageText.text = "Stage " + stageNum + "\nOver!";
        score += (int)playerS.hp * 100 - (int)curSick * 30;
        stageText.gameObject.SetActive(true);
        stageNum++;
        
        Invoke("StageTextOff", 1);
        if (stageNum >= 3)
        {
            Invoke("GameOver", 1.2f);
            return;
        }
        Invoke("StageStart", 1.5f);
    }

    public void GameOver()
    {
        PlayerPrefs.SetInt("Score", score);
        SceneManager.LoadScene(2);
    }

    void FadeOutOn()
    {
        fadeOut.gameObject.SetActive(true);
    }

    void FadeOutOff()
    {
        fadeOut.gameObject.SetActive(false);
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
            bossTimer += Time.deltaTime;
            bossObject.SetActive(false);
            return;
        }

        bossObject.SetActive(true);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }
}
