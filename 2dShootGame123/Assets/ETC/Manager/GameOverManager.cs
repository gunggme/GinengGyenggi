using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] int score;
    [SerializeField] string name;

    [Header("Player Input")]
    [SerializeField] InputField namePlayerName;

    [Header("rankText")]
    [SerializeField] Text[] rankText;

    [Header("RankScore or RankName")]
    [SerializeField] string[] rankNames;
    [SerializeField] int[] rankScore;

    [Header("Buttons")]
    [SerializeField] Button[] buttons;

    private void Awake()
    {
        rankNames = new string[5];
        rankScore = new int[5];

        for(int i = 0; i < 5; i++)
        {
            rankNames[i] = PlayerPrefs.GetString(i.ToString() + "RankName");
            rankScore[i] = PlayerPrefs.GetInt(i + "RankScore");

            rankText[i].text = "Rank " + (i + 1) + "." + rankNames[i] + "\nScore : " + rankScore[i];
        }
    }

    private void Update()
    {
        if(namePlayerName.text.Length > 0 && Input.GetKeyDown(KeyCode.Return))
        {
            SetNames();
        }
    }

    void SetNames()
    {
        name = namePlayerName.text;
        namePlayerName.gameObject.SetActive(false);
        foreach(Button bu in buttons)
        {
            bu.gameObject.SetActive(true);
        }
        SetRank();
    }

    void SetRank()
    {
        int tmpScore;
        string tmpName;

        for(int i = 0; i < 5; i++)
        {

            rankNames[i] = PlayerPrefs.GetString(i + "RankName");
            rankScore[i] = PlayerPrefs.GetInt(i + "RankScore");
            while (rankScore[i] < score)
            {
                tmpScore = rankScore[i];
                tmpName = rankNames[i];

                rankScore[i] = score;
                rankNames[i] = name;

                score = tmpScore;
                name = tmpName;

                PlayerPrefs.SetInt(i + "RankScore", rankScore[i]);
                PlayerPrefs.SetString(i.ToString() + "RankName", rankNames[i]);
            }
        }

        for(int i = 0; i < 5; i++)
        {
            

            rankText[i].text = "Rank " + (i + 1) + "." + rankNames[i] + "\nScore : " + rankScore[i]; 
        }
    }
}
