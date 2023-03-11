using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] public int score;
    [SerializeField] Text scoreText;

    [Header("PlayerHP")]
    [SerializeField] Player playerS;
    [SerializeField] Slider hpBar;

    [SerializeField] Slider fuerBar;

    [Header("Animation Text")]
    [SerializeField] Text aniText;

    [Header("Boss Timer")]
    [SerializeField] float bossTimer;

    [Header("Boss Object")]
    [SerializeField] GameObject bossObject;
    [SerializeField] Slider bossHPBar;
    [SerializeField] Boss bossS;

    [Header("Stage")]
    [SerializeField] int stageNum;

    private void Start()
    {
        GameStart();
    }

    private void Update()
    {
        SetScore();
        SetHPBar();
        SetFuer();
        SetBossHPBar();
        BossTimer();

        if (!bossObject.activeSelf)
        {
            bossHPBar.gameObject.SetActive(false);
        }
    }

    void GameStart()
    {
        //Stage n Start! 라는 글자가 나오게 텍스트 설정
        aniText.text = "Stage" + stageNum + "\nStart!";
        //게임이 시작된다는 텍스트 표기
        aniText.gameObject.SetActive(true);
        //텍스트 0.5초 후 비활성화 시키기
        Invoke("TextDown", 0.6f);
        //보스 쿨타임 초기화
        bossTimer = 0;
    }

    public void GameEnd()
    {
        //Stage n Over 글자가 나오게 텍스트 설정
        aniText.text = "Stage" + stageNum + "\nOver";
        //스테이지가 끝났다는 텍스트 활성화
        aniText.gameObject.SetActive(true);
        //텍스트 0.5초후 비활성화 시키기
        Invoke("TextDown", 0.6f);
        //보스 쿨타임 초기화
        bossTimer = 0;
        //조건문으로 2라운드가 넘어간다면 게임 오버 화면으로 넘어가기
        if(stageNum > 3)
        {
            Invoke("GameOver", 1.3f);
            //게임 오버 씬으로 이동시키기
            
        }
        //StageNum을 1추가
        stageNum++;
        //Stage 시작
        Invoke("GameStart", 1);
    }

    public void GameOver()
    {
        //게임오버 텍스트 출력
        aniText.text = "Game Over";
        aniText.gameObject.SetActive(true);
        //텍스트 0.5초후 비활성화
        Invoke("TextDown",0.6f);
        //씬이동 함수
        Invoke("MoveOver", 1);
    }

    void MoveOver()
    {
        SceneManager.LoadScene(2);
    }
    
    //여기까지의 공통작업
    //1. 스코어 저장시키기.
    //2. 조건문 넣기

    void TextDown()
    {
        // 텍스트 오브젝트 비활성화 시키는 함수
        aniText.gameObject.SetActive(false);
    }

    //보스 타이머
    void BossTimer()
    {
        if(bossTimer < 100)
        {
            bossTimer += Time.deltaTime;
            return;
        }

        bossObject.gameObject.SetActive(true);
        bossHPBar.gameObject.SetActive(true);
    }

    void SetBossHPBar()
    {
        bossHPBar.value = bossS.hp / 1000;
    }

    void SetScore()
    {
        scoreText.text = "Score : " + score.ToString("N0");
    }

    void SetHPBar()
    {
        hpBar.value = playerS.hp / 40;
    }

    void SetFuer()
    {
        fuerBar.value = playerS.fuer / 100;
    }
}
