using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverManager : MonoBehaviour
{
    [Header("Name")]
    [SerializeField] InputField inputName;

    [Header("Scores")]
    [SerializeField] Text scoreText; 
    [SerializeField] Text timeText;

    [SerializeField] float playerScore;
    [SerializeField] float playerTime;
    [SerializeField] string playerName;

    [SerializeField] float[] rankScore;
    [SerializeField] float[] rankTime;
    [SerializeField] string[] rankName;

    private void Awake()
    {
        playerScore = PlayerPrefs.GetFloat("curScore");
        playerTime = PlayerPrefs.GetFloat("time");
    }

    private void Start()
    {
        rankScore = new float[10];
        rankTime = new float[10];
        rankName = new string[10];
        for (int i = 0; i < 10; i++)
        {
            rankScore[i] = PlayerPrefs.GetFloat(i + "rankScore");
            rankTime[i] = PlayerPrefs.GetFloat(i + "rankTime");
            rankName[i] = PlayerPrefs.GetString(i + "rankName");
        }
    }

    private void Update()
    {
        if(inputName.text.Length > 1 && Input.GetKeyDown(KeyCode.Return) && inputName.gameObject.activeSelf)
        {
            SetName();
        }
    }

    void SetName()
    {
        playerName = inputName.text;
        inputName.gameObject.SetActive(false);

        SetRank(playerScore, playerTime, playerName);
    }

    void SetRank(float score, float time, string name)
    {
        float tmpScore;
        float tmpTime;
        string tmpName;
        for(int i = 0; i < 10; i++)
        {
            while(score > rankScore[i])
            {
                tmpScore = rankScore[i];
                tmpTime = rankTime[i];
                tmpName = rankName[i];

                rankScore[i] = score;
                rankTime[i] = tmpTime;
                rankName[i] = name;

                score = tmpScore;
                time = tmpTime;
                name = tmpName;
            }
        }

        for(int i = 0; i < 10; i++)
        {
            PlayerPrefs.SetFloat(i + "rankScore", rankScore[i]);
            PlayerPrefs.SetFloat(i + "rankTime", rankTime[i]);
            PlayerPrefs.SetString(i + "rankName", rankName[i]);
        }
    }
}
