using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] InputField inputName;
    [SerializeField] string playerName;

    [SerializeField] Text scoreText;
    [SerializeField] Text timeText;

    [SerializeField] float[] rankTime;
    [SerializeField] float[] rankScore;
    [SerializeField] string[] rankName;

    [SerializeField] float curTime;
    [SerializeField] float curScore;
    private void Awake()
    {
        rankTime = new float[10];
        rankScore = new float[10];
        rankName = new string[10];

        curTime = PlayerPrefs.GetFloat("curTime");
        curScore = PlayerPrefs.GetFloat("curScore");
    }

    private void Start()
    {
        scoreText.text = "Score : " + curScore.ToString("N0");
        timeText.text = "Time : " + curTime;
    }

    private void Update()
    {
        if(inputName.gameObject.activeSelf && inputName.text.Length > 1 && Input.GetKeyDown(KeyCode.Return))
        {
            SetName();
        }
    }

    void SetName()
    {
        playerName = inputName.text;
        inputName.gameObject.SetActive(false);
        RankSet(curScore, playerName, curTime);
    }

    void RankSet(float aurScore, string curName, float aurTime)
    {
        float tmpScore = 0;
        float tmpTime = 0;
        string tmpName = "";

        for(int i = 0; i < 10; i++)
        {
            rankTime[i] = PlayerPrefs.GetFloat(i + "rankTimer");
            rankScore[i] = PlayerPrefs.GetFloat(i + "rankScore");
            rankName[i] = PlayerPrefs.GetString(i + "rankName");
            while(curScore > rankScore[i])
            {
                tmpName = rankName[i];
                tmpTime = rankTime[i];
                tmpScore = rankScore[i];

                rankScore[i] = aurScore;
                rankName[i] = curName;
                rankTime[i] = aurTime;

                aurScore = tmpScore;
                aurTime = tmpTime;
                curName = tmpName;
            }
        }

        for(int i = 0; i < 10; i++)
        {
            PlayerPrefs.SetFloat(i + "rankTime", rankTime[i]);
            PlayerPrefs.SetFloat(i + "rankScore", rankScore[i]);
            PlayerPrefs.SetString(i + "rankName", rankName[i]);
        }
    }
}
