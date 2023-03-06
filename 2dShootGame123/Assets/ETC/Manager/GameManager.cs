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
        //StageNum �����ָ鼭 �����ϱ�
        aniText.text = "Stage " + stageNum + "\nStart!";
        aniText.gameObject.SetActive(true);
        //�ؽ�Ʈ 0.5�ʵ� ������� �����
        Invoke("AniTextDown", 0.5f);
        //���� Ÿ�̸� 0���� �ʱ�ȭ
        bossWaitDelay = 0;
    }

    public void StageOver()
    {
        //�������� ���� ȭ�� �����ֱ�
        aniText.text = "Stage " + stageNum + "\nOver";
        aniText.gameObject.SetActive(true);
        //�ؽ�Ʈ 0.5�ʵ� ������� �����
        Invoke("AniTextDown", 0.5f);
        //StageNum 1�߰�
        stageNum++;
        //���� ������ ���������� ������ ���ӿ��� ������ �̵�
        if(stageNum > 2)
        {
            //���̵�
        }
        //�ƴϸ� ���� ���� ���� �����ϱ�
        else
        {
            //����Ÿ�̸� 0���� �ʱ�ȭ
            bossWaitDelay = 0;
            //StageStart �Լ� 1�ʵ� ȣ��
            Invoke("StageStart", 1);
        }
        //score�� PlayerPrefs�� ����
    }

    public void GameOver()
    {
        //���� ���� �ؽ�Ʈ �����ֱ�
        aniText.text = "Stage \nOver";
        aniText.gameObject.SetActive(true);
        //�ؽ�Ʈ 0.5�ʵ� ������� �����
        Invoke("AniTextDown", 0.5f);
        //score�� PlayerPrefs�� ����
        PlayerPrefs.SetInt("Score", score);
        //���ӿ��� ������ �̵�
    }

    void AniTextDown()
    {
        //������ �ִϸ��̼� �ؽ�Ʈ ��Ȱ��ȭ ��Ű��
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
