using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPage : MonoBehaviour
{
    [Header("���ھ� �ؽ�Ʈ")]
    [SerializeField] Text scoreText;
    [SerializeField] Text[] maxScore;
    [SerializeField] InputField inputName;
    [SerializeField] string playerName;
    [SerializeField] float curScore;

    //����Ʈ ���ھ�
    private float[] bestScore = new float[5];
    private string[] bestName = new string[5];


    private void Awake()
    {
        curScore = PlayerPrefs.GetInt("Score");
        scoreText.text = "Score : " + curScore;
        inputName.gameObject.SetActive(true);

        for(int i = 0; i < 5; i++)
        {
            maxScore[i].text = i + 1 + PlayerPrefs.GetString(i.ToString() + "BestName") + "\nScore : " + PlayerPrefs.GetFloat(i + "BestScore");
        }
    }

    private void Update()
    {
        if (playerName.Length > 0 || Input.GetKey(KeyCode.Return))
        {
            InputNames();
        }
    }

    public void MoveMain()
    {
        SceneManager.LoadScene(0);
        PlayerPrefs.Save();
    }

    void InputNames()
    {
        playerName = inputName.text;
        inputName.gameObject.SetActive(false);
        ScoreSet(curScore, playerName);
    }

    /*1�� > 2�� > 3�� > 4�� > 5��*/

    void ScoreSet(float curScore, string curName)
    {
        //PlayerPrefs�� ���� �������
        PlayerPrefs.SetString("CurPlayerName", curName);
        PlayerPrefs.SetFloat("CurPlayerScore", curScore);

        float tmpScore = 0;
        string tmpName = "";

        for(int i = 0; i < 5; i++)
        {
            bestScore[i] = PlayerPrefs.GetFloat(i + "BestScore");
            bestName[i] = PlayerPrefs.GetString(i + "BestName");

            while (bestScore[i] < curScore)
            {
                //�ڸ� �ٲٱ�
                tmpScore = bestScore[i];
                tmpName = bestName[i];
                bestScore[i] = curScore;
                bestName[i] = curName;

                PlayerPrefs.SetFloat(i + "BestScore", curScore);
                PlayerPrefs.SetString(i.ToString() + "BestName", curName);

                //�غ�
                curScore = tmpScore;
                curName = tmpName;
            }
        }

        for(int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetFloat(i + "BestScore", bestScore[i]);
            PlayerPrefs.SetString(i.ToString() + "BestName", bestName[i]);
        }
    }

    



    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }
}
