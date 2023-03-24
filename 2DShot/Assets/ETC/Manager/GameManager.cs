using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public ObjectManager objMana;
    public SpawnManager spawnMana;

    public Player playerS;

    [Header("Player")]
    [SerializeField]
    Image[] hpImage;

    [Header("Boss")]
    [SerializeField] Stage1Boss boss1;
    [SerializeField] GameObject boss1Obj;
    [SerializeField] Slider boss1Hp;

    [SerializeField] Stage2Boss boss2;
    [SerializeField] GameObject boss2Obj;
    [SerializeField] Slider boss2Hp;

    [Header("BossTimer")]
    public bool bossOn;
    public bool isTime;
    [SerializeField] float bossTimer;

    [Header("Score")]
    public float score = 0;

    [Header("Stage")]
    public int stageNum;
    [SerializeField] Text stageText;

    [Header("StageOver")]
    [SerializeField] Animator overView;
    [SerializeField] Text curScore;
    [SerializeField] Text timeText;
    [SerializeField] Text bonusScore;
    [SerializeField] int bonus;
    [SerializeField] float timer;

    private void Awake()
    {
        instance = this;    
    }

    private void Start()
    {
        stageNum = 0;
        StageStart();
    }

    private void Update()
    {
        if(boss1Hp.gameObject.activeSelf && boss1Hp.value <= 0)
        {
            boss1Hp.gameObject.SetActive(false);
            StartCoroutine(Stage1Over());
        }
        if(boss2Hp.gameObject.activeSelf && boss1Hp.value <= 0)
        {
            boss2Hp.gameObject.SetActive(false);
            StartCoroutine(Stage2Over());
        }
        if (!isTime)
        {
            timer += Time.deltaTime;
        }

        BossTimer();

        BossHP1();
        BossHP2();
    }
    void StageStart()
    {
        stageNum++;
        stageText.text = "Stage " + stageNum + "\nStart!";
        stageText.gameObject.SetActive(true);
        spawnMana.isSpawn = true;
        isTime = false;
        bossOn = false;
        Invoke("StageTextDown", 0.8f);
    }

    public IEnumerator Stage1Over()
    {
        stageText.text = "Stage 1 \n Over";
        stageText.gameObject.SetActive(true);
        spawnMana.isSpawn = false;
        bossTimer = 0;
        isTime = true;
        Invoke("StageTextDown", 0.8f);
        yield return new WaitForSeconds(0.8f);
        curScore.text = "Score : " + score.ToString("N0");
        bonusScore.text = "Score + " + (playerS.hp * 1000 + playerS.fuer * 100).ToString("N0");
        timeText.text = "Time : " + Mathf.Round(timer / 100) * 100;
        overView.SetTrigger("Open");
    }

    IEnumerator Stage2Over()
    {
        stageText.text = "Stage 2 \n Over";
        stageText.gameObject.SetActive(true);
        spawnMana.isSpawn = false;
        isTime = true;
        bossTimer = 0;
        Invoke("StageTextDown", 0.8f);
        yield return new WaitForSeconds(0.8f);
        stageText.text = "Game \n Over";
        stageText.gameObject.SetActive(true);
        Invoke("StageTextDown", 0.8f);
        yield return new WaitForSeconds(0.8f);
        curScore.text = "Score : " + score.ToString("N0");
        bonusScore.text = "Score + " + (playerS.hp * 1000 + playerS.fuer * 100).ToString("N0");
        timeText.text = "Time : " + Mathf.Round(timer / 10) * 10;
        overView.SetTrigger("Open");
    }

    public void AsStartStage()
    {
        overView.SetTrigger("Close");
        switch (stageNum)
        {
            case 1:
                Invoke("StageStart", 1);
                break;
            case 2:
                Invoke("StageOver", 1);
                break;
        }
    }

    void StageOver()
    {
        //¾À ÀÌµ¿
        SceneManager.LoadScene(2);
    }

    void StageTextDown()
    {
        stageText.gameObject.SetActive(false);
    }

    public void HPSet()
    {
        switch (playerS.hp)
        {
            case 0:
                hpImage[0].color = new Color(1, 1, 1, 0);
                hpImage[1].color = new Color(1, 1, 1, 0);
                hpImage[2].color = new Color(1, 1, 1, 0);
                hpImage[3].color = new Color(1, 1, 1, 0);
                hpImage[4].color = new Color(1, 1, 1, 0);
                break;
            case 1:
                hpImage[0].color = new Color(1, 1, 1, 1);
                hpImage[1].color = new Color(1, 1, 1, 0);
                hpImage[2].color = new Color(1, 1, 1, 0);
                hpImage[3].color = new Color(1, 1, 1, 0);
                hpImage[4].color = new Color(1, 1, 1, 0);
                break;
            case 2:
                hpImage[0].color = new Color(1, 1, 1, 1);
                hpImage[1].color = new Color(1, 1, 1, 1);
                hpImage[2].color = new Color(1, 1, 1, 0);
                hpImage[3].color = new Color(1, 1, 1, 0);
                hpImage[4].color = new Color(1, 1, 1, 0);
                break;
            case 3:
                hpImage[0].color = new Color(1, 1, 1, 1);
                hpImage[1].color = new Color(1, 1, 1, 1);
                hpImage[2].color = new Color(1, 1, 1, 1);
                hpImage[3].color = new Color(1, 1, 1, 0);
                hpImage[4].color = new Color(1, 1, 1, 0);
                break;
            case 4:
                hpImage[0].color = new Color(1, 1, 1, 1);
                hpImage[1].color = new Color(1, 1, 1, 1);
                hpImage[2].color = new Color(1, 1, 1, 1);
                hpImage[3].color = new Color(1, 1, 1, 1);
                hpImage[4].color = new Color(1, 1, 1, 0);
                break;
            case 5:
                hpImage[0].color = new Color(1, 1, 1, 1);
                hpImage[1].color = new Color(1, 1, 1, 1);
                hpImage[2].color = new Color(1, 1, 1, 1);
                hpImage[3].color = new Color(1, 1, 1, 1);
                hpImage[4].color = new Color(1, 1, 1, 1);
                break;
        }
    }

    void BossHP1()
    {
        boss1Hp.value = boss1.hp / 1500;
    }

    void BossHP2()
    {
        boss2Hp.value = boss2.hp / 2500;
    }

    void BossTimer()
    {
        if (!isTime)
        {
            if(bossTimer < 100)
            {
                bossTimer += Time.deltaTime;
                return;
            }
        }

        if (!bossOn)
        {
            switch (stageNum)
            {
                case 1:
                    boss1Obj.SetActive(true);
                    boss1Hp.gameObject.SetActive(true);
                    bossOn = true;
                    break;
                case 2:
                    boss2Obj.SetActive(true);
                    boss2Hp.gameObject.SetActive(true);
                    bossOn = true;
                    break;
            }
        }
    }
    
}
