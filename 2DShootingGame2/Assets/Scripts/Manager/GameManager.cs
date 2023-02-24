using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] Text playerHPText;
    [SerializeField] Slider playerHPSlider;
    [SerializeField] Player playerS;

    [Header("Sick")]
    [SerializeField] Text sickText;
    [SerializeField] Slider sickHPSlider;
    [SerializeField] public float curSick;

    [Header("Score")]
    [SerializeField] public int score;
    [SerializeField] Text scoreText;

    [Header("Boss")]
    [SerializeField] GameObject bossObject;
    [Header("Boss Timer")]
    [SerializeField] float bossTimer;

    [Header("Stage")]
    [SerializeField] int stageNum;
    [SerializeField] Text stageText;

    private void Awake()
    {
        stageNum = 1;
    }

    private void Start()
    {
        Invoke("StageStart", 0.3f);
    }

    private void Update()
    {
        SetPlayerHPUI();
        ScoreUISet();
        BossTime();
    }

    void StageStart()
    {
        stageText.text = "Stage " + stageNum + "Start!";
        stageText.gameObject.SetActive(true);

        Invoke("StageTextOff", 0.5f);
    }

    void StageTextOff()
    {
        //stageText끄기
        stageText.gameObject.SetActive(false);
    }
    
    public void StageEnd()
    {
        //텍스트 세팅
        stageText.text = "Stage " + stageNum + "End";
        //텍스트 시작
        stageText.gameObject.SetActive(true);
        //텍스트 끄기
        Invoke("StageTextOff", 0.5f);
        //stageNum + 1추가
        stageNum++;
        //만약 stageNum이 2가 넘어간다면
        if(stageNum > 2)
        {
            //게임오버 페이지로 이동
            StageOver();
            return;
        }
        //Stage시작
        Invoke("StageStart", 1f);
        //BossTimer 초기화
        bossTimer = 0;
    }

    void StageOver()
    {
        //스테이지 이동
        //점수를 PlayerPrefs로 저장
    }

    void SetPlayerHPUI()
    {
        playerHPText.text = "PlayerHP : " + playerS.hp;
        playerHPSlider.value = playerS.hp / 30;
    }

    void ScoreUISet()
    {
        scoreText.text = "Score : " + score;
    }

    void SickUISet()
    {
        sickHPSlider.value = curSick / 100;
        sickText.text = "고통게이지 : " + curSick;
    }

    void BossTime()
    {
        //만약 bossTimer가 100초 보다 적을때 실행
        if(bossTimer < 100)
        {
            bossTimer += Time.deltaTime;
            return;
        }
        //bossTimer가 100초가 넘었을때 bossObject활성화
        bossObject.gameObject.SetActive(true);
    }
}
