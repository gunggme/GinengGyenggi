using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OverManager : MonoBehaviour
{
    [SerializeField] InputField inputName;

    [SerializeField] float[] rankScore;
    [SerializeField] float[] rankTime;
    [SerializeField] string[] rankName;

    [SerializeField] float playerScore;
    [SerializeField] float playerTime;
    [SerializeField] string playerName;

    [SerializeField] Text playerScoreText;
    [SerializeField] Text playerTimeText;

    [SerializeField] Button[] buttons;

    [Header("Back")]
    [SerializeField] GameObject backGround;

    private void Awake()
    {
        rankName = new string[10];
        rankScore = new float[10];
        rankTime = new float[10];

        playerScore = PlayerPrefs.GetFloat("curScore");
        playerTime = PlayerPrefs.GetFloat("curTime"); 
    }

    private void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            rankName[i] = PlayerPrefs.GetString(i + "rankName");
            rankScore[i] = PlayerPrefs.GetFloat(i + "rankScore");
            rankTime[i] = PlayerPrefs.GetFloat(i + "rankTime");
        }

        playerTimeText.text = "Time : " + playerTime;
        playerScoreText.text = "Score : " + playerScore.ToString("N0");
    }

    private void Update()
    {
        if(inputName.text.Length > 1 && inputName.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            SetName();
        }
    }

    void SetName()
    {
        playerName = inputName.text;
        inputName.gameObject.SetActive(false);
        SetRank(playerScore, playerName, playerTime);
        buttons[0].gameObject.SetActive(true);
        buttons[1].gameObject.SetActive(true);
    }

    void SetRank(float score, string name, float time)
    {
        float tmpTime;
        float tmpScore;
        string tmpName;
        

        for(int i = 0; i < 10; i++)
        {
            while(score > rankScore[i])
            {
                tmpTime = rankTime[i];
                tmpScore = rankScore[i];
                tmpName = rankName[i];

                rankTime[i] = time;
                rankScore[i] = score;
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

    void Close()
    {
        backGround.GetComponent<Animator>().SetTrigger("Close");
    }

    public void StartGoHome()
    {
        Close();
        Invoke("GoHome", 1);
    }


    void GoHome()
    {
        SceneManager.LoadScene(0);
    }

    public void StartGoRestart()
    {
        Close();
        Invoke("Restart", 1);
    }

    void Restart()
    {
        SceneManager.LoadScene(1);
    }
}
