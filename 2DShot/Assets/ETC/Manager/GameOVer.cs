using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOVer : MonoBehaviour
{
    [SerializeField] InputField inputName;

    [SerializeField] Text scoreText;
    [SerializeField] float score;
    [SerializeField] Text timeText;
    [SerializeField] float time;
    [SerializeField] string playerName;

    [SerializeField] Button goHome;
    [SerializeField] Button reStart;

    float[] rankScore;
    string[] rankName;

    private void Awake()
    {
        rankScore = new float[10];
        rankName = new string[10];
        score = PlayerPrefs.GetFloat("curScore");
        time = PlayerPrefs.GetFloat("Time");
    }

    private void Start()
    {
        scoreText.text = "Score : " + score.ToString("N0");
        timeText.text = "Time : " + Mathf.Round(time);
    }

    private void Update()
    {
        if (inputName.gameObject.activeSelf && inputName.text.Length > 1 && Input.GetKeyDown(KeyCode.Return))
        {
            SetName();
        }
    }

    void SetName()
    {
        playerName = inputName.text;
        inputName.gameObject.SetActive(false);
        goHome.gameObject.SetActive(true);
        reStart.gameObject.SetActive(true);
        RankSet(score, playerName);
    }

    void RankSet(float curScore, string curName)
    {
        float tmpScore = 0;
        string tmpName = "";
        for(int i = 0; i < 10; i++)
        {
            rankScore[i] = PlayerPrefs.GetFloat(i + "rankScore");
            rankName[i] = PlayerPrefs.GetString(i + "rankName");
            while(curScore > rankScore[i])
            {
                tmpScore = rankScore[i];
                tmpName = rankName[i];
                rankScore[i] = curScore;
                rankName[i] = curName;
                curScore = tmpScore;
                curName = tmpName;
            }
        }

        for(int i = 0; i < 10; i++)
        {
            PlayerPrefs.SetFloat(i + "rankScore", rankScore[i]);
            PlayerPrefs.SetString(i + "rankName", rankName[i]);
        }
    }
}
