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

    int[] rankScore;
    string[] rankName;

    private void Awake()
    {
        curScore = PlayerPrefs.GetInt("curScore");
        Time.timeScale = 1f;
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

    }
}
