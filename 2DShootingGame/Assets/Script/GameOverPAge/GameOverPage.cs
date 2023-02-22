using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPage : MonoBehaviour
{
    [Header("���ھ� �ؽ�Ʈ")]
    [SerializeField] Text scoreText;
    [SerializeField] Text maxScore;
    [SerializeField] int maxScore1 = 0;
    [SerializeField] int curScore;
    [SerializeField] InputField inputName;
    [SerializeField] string maxPlayerName;

    private void Awake()
    {
        //curScore�� Score����
        curScore = PlayerPrefs.GetInt("Score");
        //���� curScore�� maxScore1���� ũ�ٸ�?
        if (curScore > maxScore1)
        {
            //�̸� ���°� Ȱ��ȭ
            inputName.gameObject.SetActive(true);
            //MaxScore�̸��� ���� PlayerPrefs���� curScore�� ����
            PlayerPrefs.SetInt("MaxScore", curScore);
        }
        //���� MaxScore�̸��� ���� Key�� �ִٸ�
        if (PlayerPrefs.HasKey("MaxScore"))
        {
            //�÷��̾� ������ ��ü ����
            PlayerPrefs.Save();
            //maxScore1���� MaxScore�� �����Ѵ�.
            maxScore1 = PlayerPrefs.GetInt("MaxScore");
            //���� maxScore1�� 0�̶�� 0���� �����ϰ� �������´�. 
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
        //scoreText���ٰ� Score : ���� ������ ����
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
