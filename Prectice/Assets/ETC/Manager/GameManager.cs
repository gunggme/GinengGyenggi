using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public ObjectManager objMana;
    public SpawnManager spawnMana;

    public Player player;
    [Header("HP")]
    [SerializeField]
    Image[] hpImage;

    [Header("Score")]
    public float score;
    [SerializeField] Text scoreText;

    [Header("Stage")]
    public int stageNum;
    [SerializeField] Text stageText;


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
        
    }

    void StageStart()
    {
        stageNum++;
        stageText.text = "Stage " + stageNum + "\nStart!";
        stageText.gameObject.SetActive(true);
        Invoke("TextDown", 0.8f);
    }

    void TextDown()
    {
        stageText.gameObject.SetActive(false);
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
