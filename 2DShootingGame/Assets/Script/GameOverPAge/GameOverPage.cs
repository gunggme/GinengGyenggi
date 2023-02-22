using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPage : MonoBehaviour
{
    [Header("스코어 텍스트")]
    [SerializeField] Text scoreText;
    [SerializeField] Text maxScore;
    [SerializeField] int maxScore1 = 0;
    [SerializeField] int curScore;
    [SerializeField] InputField inputName;
    [SerializeField] string maxPlayerName;

    private void Awake()
    {
        maxPlayerName = PlayerPrefs.GetString("MaxScorePlayerName");
        if(maxPlayerName == null)
        {
            maxPlayerName = "Null";
        } 
        curScore = PlayerPrefs.GetInt("Score");
        if (curScore > maxScore1)
        {
            inputName.gameObject.SetActive(true);
            PlayerPrefs.SetInt("MaxScore", curScore);
        }
        maxScore1 = PlayerPrefs.GetInt("MaxScore");
        
        scoreText.text = "Score : " + curScore;
        maxScore.text = maxPlayerName + "\nScore : " + maxScore;
    }


}
