using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [Header("Cur Scor && Name")]
    [SerializeField] string userName;
    [SerializeField] InputField inputName;
    [SerializeField] Text scoreText;
    [SerializeField] int curScore;

    [Header("RankScore || Name")]
    [SerializeField] string[] names;
    [SerializeField] int[] scores;
    [SerializeField] Text[] texts;

    private void Awake()
    {
        names = new string[5];
        scores = new int[5];
        curScore = PlayerPrefs.GetInt("CurScore");
    }

    private void Start()
    {
        scoreText.text = "Score : " + curScore.ToString("N0");
        for (int i = 0; i < 5; i++)
        {
            scores[i] = PlayerPrefs.GetInt(i + "RankScore");
            names[i] = PlayerPrefs.GetString(i + "RankName");

            texts[i].text = "Rank " + (i + 1) + ". " + names[i] + "\nScore : " + scores[i];
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            Debug.Log("123");
            
        //SetName();
    }

    void SetName()
    {
        
        userName = inputName.text;
        SetRankText(curScore, userName);
    }

    void SetRankText(int score, string name)
    {
        int tmpScore = 0;
        string tmpName;
        for(int i = 0; i < 5; i++)
        {
            scores[i] = PlayerPrefs.GetInt(i + "RankScore");
            names[i] = PlayerPrefs.GetString(i + "RankName");

            while (scores[i] < score)
            {
                tmpScore = scores[i];
                tmpName = names[i];

                scores[i] = score;
                names[i] = name;

                score = tmpScore;
                name = tmpName;
            }
        }

        for(int i = 0; i < 5; i++)
        {
            texts[i].text = "Rank " + (i + 1) + names[i] + "\nScore : " + scores[i];
            PlayerPrefs.SetInt(i + "RankScore", scores[i]);
            PlayerPrefs.SetString(i.ToString() + "RankName", names[i]);
        }
    }
}
