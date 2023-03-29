using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public SpawnManager spawnMana;
    public ObjectManager objMana;
    public Player player;

    [Header("Time")]
    public float time;

    [Header("Score")]
    public float score;
    [SerializeField] Text scoreText;

    [Header("HP")]
    [SerializeField] Image[] hpImgs;
    [SerializeField] Slider fuerBar;

    [Header("Boss")]
    [SerializeField] Stage1Boss boss1;
    [SerializeField] GameObject boss1Obj;
    [SerializeField] Slider boss1HP;

    [Header("Boss2")]
    [SerializeField] Stage2Boss boss2;
    [SerializeField] GameObject boss2Obj;
    [SerializeField] Slider boss2HP;

    [Header("BossTimer")]
    [SerializeField] float bossTimer;
    [SerializeField] bool isH;
    [SerializeField] bool isBoss;

    [Header("Stage")]
    public int stageNum;
    [SerializeField] Text stageAni;

    [Header("StageOver")]
    [SerializeField] GameObject overScene;
    [SerializeField] Text scoreText1;
    [SerializeField] Text bonusText;
    [SerializeField] Text timeText;
    [SerializeField] float bonus;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StageStart();
    }

    private void Update()
    {
        Fuer();
        BossTime();
        Boss1HPSet();
        Boss2HPSet();

        scoreText.text = "Score : " + score.ToString("N0");

        if (isH)
        {
            time += Time.deltaTime;
        }

        if(boss1HP.value <= 0 && boss1HP.gameObject.activeSelf)
        {
            StartCoroutine("Stage1Over");
        }
        else if(boss2HP.value <= 0 && boss2HP.gameObject.activeSelf)
        {
            StartCoroutine("Stage2Over");
        }
        
    }

    void StageStart()
    {
        isBoss = false;
        isH = true;
        stageNum++;
        stageAni.text = "Stage " + stageNum + "\nStart";
        stageAni.gameObject.SetActive(true);
        Invoke("TextFalse", 0.5f);
    }

    void TextFalse()
    {
        stageAni.gameObject.SetActive(false);
    }

    public void asStart()
    {
        overScene.GetComponent<Animator>().SetTrigger("Close");
        spawnMana.isSpawn = true;
        switch (stageNum)
        {
            case 1:
                Invoke("StageStart", 1);
                break;
            case 2:
                //Scene¿Ãµø
                Invoke("SceneMove", 1);
                break;
        }
    }

    public IEnumerator Stage1Over()
    {
        boss1HP.gameObject.SetActive(false);
        stageAni.text = "Stage 1\nOver";
        bonus = player.hp * 100 * player.fuer;
        stageAni.gameObject.SetActive(true);
        bossTimer = 0;
        Invoke("TextFalse", 0.5f);
        isH = false;
        isBoss = false;
        bossTimer = 0;
        yield return new WaitForSeconds(0.5f);
        scoreText1.text = "Score : " + score.ToString("N0");
        bonusText.text = "Score + " + bonus.ToString("N0");
        timeText.text = "Time : " + time.ToString("N0");
        score += bonus;
        overScene.GetComponent<Animator>().SetTrigger("Open");
    }

    IEnumerator Stage2Over()
    {
        stageAni.text = "Stage 2\nOver";
        boss2HP.gameObject.SetActive(false);
        bonus = player.hp * 100 * player.fuer;
        stageAni.gameObject.SetActive(true);
        Invoke("TextFalse", 0.5f);
        isH = false;
        isBoss = false;
        bossTimer = 0;
        PlayerPrefs.SetFloat("curScore", score);
        PlayerPrefs.SetFloat("Time", time);
        yield return new WaitForSeconds(0.5f);
        stageAni.text = "Game\nOver";
        stageAni.gameObject.SetActive(true);
        Invoke("TextFalse", 0.5f);
        yield return new WaitForSeconds(0.5f);
        score += bonus;
        scoreText1.text = "Score : " + score.ToString("N0");
        bonusText.text = "Score + " + bonus.ToString("N0");
        timeText.text = "Time : " + time.ToString("N0");
        overScene.GetComponent<Animator>().SetTrigger("Open");
    }

    void SceneMove()
    {
        SceneManager.LoadScene(2);
    }

    void Boss1HPSet()
    {
        boss1HP.value = boss1.hp / 1000;
    }

    void Boss2HPSet()
    {
        boss2HP.value = boss2.hp / 2000;
    }

    void Fuer()
    {
        fuerBar.value = player.fuer / 100;
    }

    void BossTime()
    {
        if (isH)
        {
            if(bossTimer < 100)
            {
                bossTimer += Time.deltaTime;
                return;
            }

            if (!isBoss)
            {
                switch(stageNum)
                {
                    case 1:
                        boss1Obj.gameObject.SetActive(true);
                        boss1HP.gameObject.SetActive(true);
                        spawnMana.isSpawn = false;
                        isBoss = true;
                        break;
                    case 2:
                        spawnMana.isSpawn = false;
                        boss2Obj.gameObject.SetActive(true);
                        boss2HP.gameObject.SetActive(true);
                        isBoss = true;
                        break;
                }
            }
        }
    }

    public void HPSet()
    {
        switch (player.hp)
        {
            case 0:
                hpImgs[0].color = new Color(1, 1, 1, 0);
                hpImgs[1].color = new Color(1, 1, 1, 0);
                hpImgs[2].color = new Color(1, 1, 1, 0);
                hpImgs[3].color = new Color(1, 1, 1, 0);
                hpImgs[4].color = new Color(1, 1, 1, 0);
                break;
            case 1:
                hpImgs[0].color = new Color(1, 1, 1, 1);
                hpImgs[1].color = new Color(1, 1, 1, 0);
                hpImgs[2].color = new Color(1, 1, 1, 0);
                hpImgs[3].color = new Color(1, 1, 1, 0);
                hpImgs[4].color = new Color(1, 1, 1, 0);
                break;
            case 2:
                hpImgs[0].color = new Color(1, 1, 1, 1);
                hpImgs[1].color = new Color(1, 1, 1, 1);
                hpImgs[2].color = new Color(1, 1, 1, 0);
                hpImgs[3].color = new Color(1, 1, 1, 0);
                hpImgs[4].color = new Color(1, 1, 1, 0);
                break;
            case 3:
                hpImgs[0].color = new Color(1, 1, 1, 1);
                hpImgs[1].color = new Color(1, 1, 1, 1);
                hpImgs[2].color = new Color(1, 1, 1, 1);
                hpImgs[3].color = new Color(1, 1, 1, 0);
                hpImgs[4].color = new Color(1, 1, 1, 0);
                break;
            case 4:
                hpImgs[0].color = new Color(1, 1, 1, 1);
                hpImgs[1].color = new Color(1, 1, 1, 1);
                hpImgs[2].color = new Color(1, 1, 1, 1);
                hpImgs[3].color = new Color(1, 1, 1, 1);
                hpImgs[4].color = new Color(1, 1, 1, 0);
                break;
            case 5:
                hpImgs[0].color = new Color(1, 1, 1, 1);
                hpImgs[1].color = new Color(1, 1, 1, 1);
                hpImgs[2].color = new Color(1, 1, 1, 1);
                hpImgs[3].color = new Color(1, 1, 1, 1);
                hpImgs[4].color = new Color(1, 1, 1, 1);
                break;
        }
    }
}
