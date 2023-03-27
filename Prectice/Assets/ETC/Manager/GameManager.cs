using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public ObjectManager objMana;
    public SpawnManager spawnMana;

    public Player player;
    [SerializeField] GameObject playerObj;
    [Header("HP")]
    [SerializeField]
    Image[] hpImage;
    [SerializeField] Slider fuerBar;

    [Header("Score")]
    public float score;
    [SerializeField] Text scoreText;

    [Header("Stage")]
    public int stageNum;
    [SerializeField] Text stageText;

    [Header("Scene")]
    [SerializeField] GameObject scene;

    [Header("Boss")]
    [SerializeField] Stage1Boss boss1;
    [SerializeField] GameObject boss1Obj;
    [SerializeField] Slider boss1HP;

    [SerializeField] Stage2Boss boss2;
    [SerializeField] GameObject boss2Obj;
    [SerializeField] Slider boss2HP;

    [Header("BossTimer")]
    [SerializeField] float bossTimer;
    [SerializeField] bool isBossOn;
    [SerializeField] bool isTimeBoss;

    [Header("Timer")]
    [SerializeField] float timer;
    [SerializeField] bool isTime;

    [Header("GameClear")]
    [SerializeField] Text scoreText1;
    [SerializeField] Text bonusScore;
    [SerializeField] Text timerText;
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
        ScoreSet();
        BossOn();
        Boss1HP();
        Boss2HP();
        if (isTime)
        {
            timer += Time.deltaTime;
        }

        if(boss1HP.value <= 0 && boss1HP.gameObject.activeSelf)
        {
            StartCoroutine(Stage1Clear());
        }
        if(boss2HP.value <= 0 && boss2HP.gameObject.activeSelf)
        {
            StartCoroutine("Stage2Clear");
        }

        fuerBar.value = player.fuer / 100;
        if(fuerBar.value < 0.01)
        {
            StartCoroutine(GameOver());
        }
    }

    void StageStart()
    {
        stageNum++;
        stageText.text = "Stage " + stageNum + "\nStart!";
        stageText.gameObject.SetActive(true);
        isTimeBoss = true;
        isTime = true;
        spawnMana.isSpawn = true;   
        Invoke("TextDown", 0.8f);
    }

    void TextDown()
    {
        stageText.gameObject.SetActive(false);
    }

    IEnumerator Stage1Clear()
    {
        bonus = player.hp * 100 * player.fuer;
        boss1HP.gameObject.SetActive(false);
        stageText.text = "Stage " + stageNum + "\nClear!";
        scoreText1.text = "Score : " + score.ToString("N0");
        bonusScore.text = "Score + " + bonus.ToString("N0");
        timerText.text = "Time : " + timer;
        spawnMana.isSpawn = false;
        isBossOn = true;
        isTimeBoss = false;
        bossTimer = 0;
        isTime = false;
        stageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        stageText.gameObject.SetActive(false);
        score += bonus;
        scene.GetComponent<Animator>().SetTrigger("Open");
    }

    IEnumerator Stage2Clear()
    {
        bonus = player.hp * 100 * player.fuer;
        boss2HP.gameObject.SetActive(false);
        stageText.text = "Stage " + stageNum + "\nClear!";
        scoreText1.text = "Score : " + score.ToString("N0");
        bonusScore.text = "Score + " + bonus.ToString("N0");
        timerText.text = "Time : " + timer;
        spawnMana.isSpawn = false;
        isBossOn = true;
        isTimeBoss = false;
        bossTimer = 0;
        isTime = false;
        stageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        stageText.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        stageText.text = "Game Clear!";
        stageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        stageText.gameObject.SetActive(false);
        score += bonus;
        scene.GetComponent<Animator>().SetTrigger("Open");
    }

    IEnumerator GameOver()
    {
        playerObj.gameObject.SetActive(false);
        stageText.text = "Game Over!";
        stageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        stageText.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(2);
    }

    public void AsStart()
    {
        scene.GetComponent<Animator>().SetTrigger("Close");
        switch(stageNum)
        {
            case 1:
                Invoke("StageStart", 1f);
                break;
            case 2:
                //æ¿ ¿Ãµø
                Invoke("OverScene", 1f);
                break;
        }
    }

    void OverScene()
    {
        SceneManager.LoadScene(2);
    }

    void Boss1HP()
    {
        boss1HP.value = boss1.hp / 1000;
    }

    void Boss2HP()
    {
        boss2HP.value = boss2.hp / 1000;
    }

    void BossOn()
    {
        if (isTimeBoss)
        {
            if (bossTimer < 100)
            {
                bossTimer += Time.deltaTime;
                return;
            }
            if (isBossOn)
            {
                switch(stageNum)
                {
                    case 1:
                        boss1Obj.gameObject.SetActive(true);
                        boss1HP.gameObject.SetActive(true);
                        isBossOn = false;
                        break;
                    case 2:
                        boss2Obj.gameObject.SetActive(true);
                        boss2HP.gameObject.SetActive(true);
                        isBossOn = false;
                        break;
                }
            }
        }
    }

    void ScoreSet()
    {
        scoreText.text = "Score : " + score.ToString("N0");
    }

    public void HPSet()
    {
        switch (player.hp)
        {
            case 0:
                StartCoroutine("GameOver");
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
}
