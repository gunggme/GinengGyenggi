using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [Header("Player Name InputField")]
    [SerializeField] InputField inputName;
    [SerializeField] int curScore;
    [SerializeField] string curName;
    [SerializeField] Text curScoreText;

    [Header("Rank Text")]
    [SerializeField] Text[] rankText;

    int[] rankScore;
    string[] rankName;

    private void Awake()
    {
        rankScore = new int[5];
        rankName = new string[5];   
        curScore = PlayerPrefs.GetInt("curScore");
        Time.timeScale = 1f;
    }

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            rankScore[i] = PlayerPrefs.GetInt(i + "BestScore");
            rankName[i] = PlayerPrefs.GetString(i.ToString() + "BestName");

            rankText[i].text = (i + 1) + ". " + rankName[i] + "\nScore : " + rankScore[i];
        }
        curScoreText.text = "Score : " + curScore;
    }

    private void Update()
    {
        if(inputName.text.Length > 0 && Input.GetKeyDown(KeyCode.Return))
        {
            SetName();
        }
    }

    void SetName()
    {
        curName = inputName.text;
        inputName.gameObject.SetActive(false);
        SetRank(curScore, curName);
    }

    void SetRank(int curScore, string curName)
    {
        int tmpScore = 0;
        string tmpName = "";

        for(int i = 0; i < 5; i++)
        {
            rankName[i] = PlayerPrefs.GetString(i + "BestName");
            rankScore[i] = PlayerPrefs.GetInt(i + "BestScore");
            while (rankScore[i] < curScore)
            {
                tmpScore = rankScore[i];
                tmpName = rankName[i];

                rankScore[i] = curScore;
                rankName[i] = curName;

                curScore = tmpScore;
                curName = tmpName;
            }
        }

        for(int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetInt(i + "BestScore", rankScore[i]);
            PlayerPrefs.SetString(i.ToString() + "BestName", rankName[i]);

            rankText[i].text = (i + 1) + ". " + rankName[i] + "\nScore : " + rankScore[i];
        }
    }
}
