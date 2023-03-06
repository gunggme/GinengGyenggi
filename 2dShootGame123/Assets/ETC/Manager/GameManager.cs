using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] Player playerS;
    [SerializeField] Slider playerDurabilityBar;
    [SerializeField] Slider playerOilBar;

    [Header("Score")]
    [SerializeField] public int score;

    [Header("Boss")]
    [SerializeField] float bossWaitDelay;
    [SerializeField] GameObject boss;
    [SerializeField] Boss bossS;
    [SerializeField] Slider bossHPBar;

    [Header("Stage")]
    [SerializeField] Text aniText;
    [SerializeField] int stageNum;

    private void Awake()
    {
        stageNum = 1;
    }

    private void Start()
    {
        StageStart();
    }


    private void Update()
    {
        setDurability();
        oilSet();
        BossHPSet();
        BossSet();
    }

    void StageStart()
    {
        //StageNum 보여주면서 시작하기
        aniText.text = "Stage " + stageNum + "\nStart!";
        aniText.gameObject.SetActive(true);
        //텍스트 0.5초뒤 사라지게 만들기
        Invoke("AniTextDown", 0.5f);
        //보스 타이머 0으로 초기화
        bossWaitDelay = 0;
    }

    public void StageOver()
    {
        //스테이지 오버 화면 보여주기
        aniText.text = "Stage " + stageNum + "\nOver";
        aniText.gameObject.SetActive(true);
        //텍스트 0.5초뒤 사라지게 만들기
        Invoke("AniTextDown", 0.5f);
        //StageNum 1추가
        stageNum++;
        //만약 정해진 스테이지가 넘으면 게임오버 씬으로 이동
        if(stageNum > 2)
        {
            //씬이동
        }
        //아니면 게임 다음 라운드 시작하기
        else
        {
            //보스타이머 0으로 초기화
            bossWaitDelay = 0;
            //StageStart 함수 1초뒤 호출
            Invoke("StageStart", 1);
        }
        //score를 PlayerPrefs로 저장
    }

    public void GameOver()
    {
        //게임 오버 텍스트 보여주기
        aniText.text = "Stage \nOver";
        aniText.gameObject.SetActive(true);
        //텍스트 0.5초뒤 사라지게 만들기
        Invoke("AniTextDown", 0.5f);
        //score를 PlayerPrefs로 저장
        PlayerPrefs.SetInt("Score", score);
        //게임오버 씬으로 이동
    }

    void AniTextDown()
    {
        //지정한 애니메이션 텍스트 비활성화 시키기
        aniText.gameObject.SetActive(false);
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
