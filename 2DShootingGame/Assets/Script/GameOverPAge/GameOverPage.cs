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
        //curScore에 Score저장
        curScore = PlayerPrefs.GetInt("Score");
        //만약 curScore가 maxScore1보다 크다면?
        if (curScore > maxScore1)
        {
            //이름 적는곳 활성화
            inputName.gameObject.SetActive(true);
            //MaxScore이름을 가진 PlayerPrefs에다 curScore을 저장
            PlayerPrefs.SetInt("MaxScore", curScore);
        }
        //만약 MaxScore이름을 가진 Key가 있다면
        if (PlayerPrefs.HasKey("MaxScore"))
        {
            //플레이어 프렙스 전체 저장
            PlayerPrefs.Save();
            //maxScore1에다 MaxScore을 저장한다.
            maxScore1 = PlayerPrefs.GetInt("MaxScore");
            //만약 maxScore1이 0이라면 0으로 저장하고 빠져나온다. 
            if (maxScore1 == 0)
            {
                maxScore1 = 0;
                return;
            }
            return;
        }
            
        
        
    }

    private void Start()
    {
        //scoreText에다가 Score : 현재 점수를 저장
        scoreText.text = "Score : " + curScore;
        maxPlayerName = PlayerPrefs.GetString("MaxScorePlayerName");
        if (maxPlayerName == null)
        {
            maxPlayerName = "Null";
        }
        maxScore.text = maxPlayerName + "\nMax Score : " + maxScore1;
    }



    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }
}
