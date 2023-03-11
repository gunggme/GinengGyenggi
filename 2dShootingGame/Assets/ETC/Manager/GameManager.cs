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
        //Stage n Start! ��� ���ڰ� ������ �ؽ�Ʈ ����
        aniText.text = "Stage" + stageNum + "\nStart!";
        //������ ���۵ȴٴ� �ؽ�Ʈ ǥ��
        aniText.gameObject.SetActive(true);
        //�ؽ�Ʈ 0.5�� �� ��Ȱ��ȭ ��Ű��
        Invoke("TextDown", 0.6f);
        //���� ��Ÿ�� �ʱ�ȭ
        bossTimer = 0;
    }

    public void GameEnd()
    {
        //Stage n Over ���ڰ� ������ �ؽ�Ʈ ����
        aniText.text = "Stage" + stageNum + "\nOver";
        //���������� �����ٴ� �ؽ�Ʈ Ȱ��ȭ
        aniText.gameObject.SetActive(true);
        //�ؽ�Ʈ 0.5���� ��Ȱ��ȭ ��Ű��
        Invoke("TextDown", 0.6f);
        //���� ��Ÿ�� �ʱ�ȭ
        bossTimer = 0;
        //���ǹ����� 2���尡 �Ѿ�ٸ� ���� ���� ȭ������ �Ѿ��
        if(stageNum > 3)
        {
            Invoke("GameOver", 1.3f);
            //���� ���� ������ �̵���Ű��
            
        }
        //StageNum�� 1�߰�
        stageNum++;
        //Stage ����
        Invoke("GameStart", 1);
    }

    public void GameOver()
    {
        //���ӿ��� �ؽ�Ʈ ���
        aniText.text = "Game Over";
        aniText.gameObject.SetActive(true);
        //�ؽ�Ʈ 0.5���� ��Ȱ��ȭ
        Invoke("TextDown",0.6f);
        //���̵� �Լ�
        Invoke("MoveOver", 1);
    }

    void MoveOver()
    {
        SceneManager.LoadScene(2);
    }
    
    //��������� �����۾�
    //1. ���ھ� �����Ű��.
    //2. ���ǹ� �ֱ�

    void TextDown()
    {
        // �ؽ�Ʈ ������Ʈ ��Ȱ��ȭ ��Ű�� �Լ�
        aniText.gameObject.SetActive(false);
    }

    //���� Ÿ�̸�
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
