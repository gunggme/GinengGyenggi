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

    public Player playerS;

    [Header("HP")]
    [SerializeField]
    Image[] playerHP;

    [Header("Boss")]
    [SerializeField] GameObject boss1;
    [SerializeField] Boss1 boss1S;
    [SerializeField] Slider boss1Bar;

    [SerializeField] GameObject boss2;
    [SerializeField] Boss2 boss2S;
    [SerializeField] Slider boss2Bar;

    [SerializeField] bool bossOn;

    [SerializeField] float bossTimer;

    [Header("Fuer")]
    [SerializeField] Slider fuerSlider;

    [Header("Score")]
    public float score;
    [SerializeField] Text scoreText;
    [SerializeField] Text scoreText2;
    [SerializeField] Text timerText;
    [SerializeField] Text bonusScoreText;
    [SerializeField] float timer;

    [SerializeField] GameObject endScene;

    [Header("Stage")]
    [SerializeField] public int stageNum;

    [Header("Text")]
    [SerializeField] Text aniText;

    bool isf = true;
    bool isp = true;

    private void Awake()
    {
        instance = this;
        stageNum =  1;
    }

    private void Start()
    {
        StageStart();
    }

    private void Update()
    {
        FuerSet();
        BossTimer();

        Boss1HP();
        Boss2HP();

        float bonus = playerS.hp * 1000 + playerS.fuer * 100;

        scoreText.text = "Score : " + score.ToString("N0");
        scoreText2.text = "Score : " + score.ToString("N0");
        timerText.text = "Time : " + Mathf.Round(timer); 
        bonusScoreText.text = "Score + " + bonus.ToString("N0");

        if(playerS.hp == 0)
        {
            StartCoroutine(GameOver());
        }
        else if(playerS.fuer == 0)
        {
            StartCoroutine(GameOver());
        }

        if (isf)
        {
            timer += Time.deltaTime;
        }
    }

    void StageStart()
    {
        isf = true;
        Debug.Log(1);
        aniText.text = "Stage " + stageNum + "\nStart!";
        aniText.gameObject.SetActive(true);
        Invoke("TextDown", 1.2f);
    }



    public void Stage1Over()
    {
        aniText.text = "Stage " + stageNum + "\nOver!";
        isf = false;
        isp = false;
        score += playerS.hp * 1000 + playerS.fuer * 100;
        aniText.gameObject.SetActive(true);
        endScene.GetComponent<Animator>().SetTrigger("On");
        stageNum++;
        playerS.isDown = false;
        //spawnMana.isSpawn = true;
        bossTimer = 0;
        boss1.gameObject.SetActive(false);
        boss1Bar.gameObject.SetActive(false);
        //bossOn = false;
        Invoke("TextDown", 1f);
    }

    public void asStart()
    {
        playerS.isDown = true;
        isp = true;
        bossOn = false;
        spawnMana.isSpawn = true;
        endScene.GetComponent<Animator>().SetTrigger("Off");
        switch (stageNum)
        {
            case 2:
                Invoke("StageStart", 0.5f);
                break;
            case 3:
                Invoke("ScenMove", 1);
                break; 
        }
    }

    void ScenMove()
    {
        SceneManager.LoadScene(2);
    }

    public IEnumerator Stage2Over()
    {
        boss2Bar.gameObject.SetActive(false);
        aniText.text = "Stage " + stageNum + "\nOver!";
        aniText.gameObject.SetActive(true);
        stageNum++;
        boss2.gameObject.SetActive(false);
        isf = false;
        bossOn = false;
        bossTimer = 0;
        playerS.isDown = false;
        boss2Bar.gameObject.SetActive(false);
        PlayerPrefs.SetFloat("curScore", score);
        PlayerPrefs.SetFloat("Time", timer);
        yield return new WaitForSeconds(1f);
        TextDown();
        yield return new WaitForSeconds(0.3f);
        aniText.text = "GameOver";
        aniText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        aniText.gameObject.SetActive(false);
        endScene.GetComponent<Animator>().SetTrigger("On");
        //씬 이동
    }

    public IEnumerator GameOver()
    {
        aniText.text = "GameOver";
        aniText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        aniText.gameObject.SetActive(false);
        PlayerPrefs.SetFloat("curScore", score);
        PlayerPrefs.SetFloat("Time", timer);
        Invoke("ScenMove", 0.2f);
    }

    void TextDown()
    {
        aniText.gameObject.SetActive(false);
    }

    public void HPSet()
    {
        //hp 설정되게 만들기
        switch (playerS.hp)
        {
            case 0:
                playerHP[0].color = new Color(1, 1, 1, 0);
                playerHP[1].color = new Color(1, 1, 1, 0);
                playerHP[2].color = new Color(1, 1, 1, 0);
                playerHP[3].color = new Color(1, 1, 1, 0);
                playerHP[4].color = new Color(1, 1, 1, 0);
                break;
            case 1:
                playerHP[0].color = new Color(1, 1, 1, 1);
                playerHP[1].color = new Color(1, 1, 1, 0);
                playerHP[2].color = new Color(1, 1, 1, 0);
                playerHP[3].color = new Color(1, 1, 1, 0);
                playerHP[4].color = new Color(1, 1, 1, 0);
                break;
            case 2:
                playerHP[0].color = new Color(1, 1, 1, 1);
                playerHP[1].color = new Color(1, 1, 1, 1);
                playerHP[2].color = new Color(1, 1, 1, 0);
                playerHP[3].color = new Color(1, 1, 1, 0);
                playerHP[4].color = new Color(1, 1, 1, 0);
                break;
            case 3:
                playerHP[0].color = new Color(1, 1, 1, 1);
                playerHP[1].color = new Color(1, 1, 1, 1);
                playerHP[2].color = new Color(1, 1, 1, 1);
                playerHP[3].color = new Color(1, 1, 1, 0);
                playerHP[4].color = new Color(1, 1, 1, 0);
                break;
            case 4:
                playerHP[0].color = new Color(1, 1, 1, 1);
                playerHP[1].color = new Color(1, 1, 1, 1);
                playerHP[2].color = new Color(1, 1, 1, 1);
                playerHP[3].color = new Color(1, 1, 1, 1);
                playerHP[4].color = new Color(1, 1, 1, 0);
                break;
            case 5:
                playerHP[0].color = new Color(1, 1, 1, 1);
                playerHP[1].color = new Color(1, 1, 1, 1);
                playerHP[2].color = new Color(1, 1, 1, 1);
                playerHP[3].color = new Color(1, 1, 1, 1);
                playerHP[4].color = new Color(1, 1, 1, 1);
                break;
        }
    }

    void FuerSet()
    {
        fuerSlider.value = playerS.fuer / 100;
    }

    void Boss1HP()
    {
        boss1Bar.value = boss1S.hp / 1000;
    }

    void Boss2HP()
    {
        boss2Bar.value = boss2S.hp / 2000;
        if(boss2Bar.gameObject.activeSelf && boss2Bar.value <= 0f)
        {
            StartCoroutine(Stage2Over());
            //boss2.gameObject.SetActive(false);
        }
    }

    void BossTimer()
    {
        if(bossTimer < 100 && isp)
        {
            bossTimer += Time.deltaTime;
            return;
        }

        if (!bossOn)
        {
            switch (stageNum)
            {
                case 1:
                    boss1.gameObject.SetActive(true);
                    boss1Bar.gameObject.SetActive(true);
                    bossOn = true;
                    break;
                case 2:
                    bossOn = true;
                    boss2.gameObject.SetActive(true);
                    boss2Bar.gameObject.SetActive(true);
                    break;
            }
        }
    }
}
