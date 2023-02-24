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
        //stageText����
        stageText.gameObject.SetActive(false);
    }
    
    public void StageEnd()
    {
        //�ؽ�Ʈ ����
        stageText.text = "Stage " + stageNum + "End";
        //�ؽ�Ʈ ����
        stageText.gameObject.SetActive(true);
        //�ؽ�Ʈ ����
        Invoke("StageTextOff", 0.5f);
        //stageNum + 1�߰�
        stageNum++;
        //���� stageNum�� 2�� �Ѿ�ٸ�
        if(stageNum > 2)
        {
            //���ӿ��� �������� �̵�
            StageOver();
            return;
        }
        //Stage����
        Invoke("StageStart", 1f);
        //BossTimer �ʱ�ȭ
        bossTimer = 0;
    }

    void StageOver()
    {
        //�������� �̵�
        //������ PlayerPrefs�� ����
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
        sickText.text = "��������� : " + curSick;
    }

    void BossTime()
    {
        //���� bossTimer�� 100�� ���� ������ ����
        if(bossTimer < 100)
        {
            bossTimer += Time.deltaTime;
            return;
        }
        //bossTimer�� 100�ʰ� �Ѿ����� bossObjectȰ��ȭ
        bossObject.gameObject.SetActive(true);
    }
}
