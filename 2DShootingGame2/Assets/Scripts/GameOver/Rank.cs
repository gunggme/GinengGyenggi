using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Rank : MonoBehaviour
{
    [Header("Score Text & InputName")]
    [SerializeField] Text scoreText;
    [SerializeField] InputField inputNameField;

    [Header("RankScoreTexts & RankNames")]
    [SerializeField] Text[] rankScoreText;

    //BestScoreName & BestScore
    string[] bestScoreName;
    int[] bestScore;

    int score;

    string playerName;

    void Awake()
    {
        bestScoreName = new string[5];
        bestScore = new int[5];
        score = PlayerPrefs.GetInt("CurScore");
    }
        
    private void Start()
    {
        scoreText.text = "Score : " + score;
    }

    private void Update()
    {
        if(inputNameField.gameObject.activeSelf && inputNameField.text.Length > 0 && Input.GetKeyDown(KeyCode.Return))
        {
            InputAccept();
        }
    }

    void InputAccept()
    {
        playerName = inputNameField.text;
        inputNameField.gameObject.SetActive(false);
        SetScore(score, playerName);
    }

    void SetScore(int score, string name)
    {
        int tmpScore = 0;
        string tmpName;

        for(int i = 0; i < 5; i++)
        {
            bestScore[i] = PlayerPrefs.GetInt(i + "BestScore");
            bestScoreName[i] = PlayerPrefs.GetString(i + "BestScoreName");

            while (bestScore[i] < score)
            {
                //자리변경
                tmpScore = bestScore[i];
                tmpName = bestScoreName[i];
                bestScore[i] = score;
                bestScoreName[i] = name;

                //다음 준비
                score = tmpScore;
                name = tmpName;
            }
        }

        for(int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetInt(i + "BestScore", bestScore[i]);
            PlayerPrefs.SetString(i.ToString() + "BestScoreName", bestScoreName[i]);
            rankScoreText[i].text = "1." + bestScoreName[i] + "\nScore : " + bestScore[i];
        }
    }
}
