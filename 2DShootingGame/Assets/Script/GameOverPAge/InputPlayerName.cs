using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputPlayerName : MonoBehaviour
{
    [SerializeField] InputField inputName;
    [SerializeField] string playerName = null;

    private void Awake()
    {
        //playerName = inputName.text;
    }

    private void Update()
    {
        if (inputName.gameObject.activeSelf || playerName.Length > 0 || Input.GetKey(KeyCode.Return))
        {
            InputNames();
        }
    }

    void InputNames()
    {
        playerName = inputName.text;
        if(playerName == null)
        {
            playerName = "Null";
        }
        PlayerPrefs.SetString("MaxScorePlayerName", playerName);
        PlayerPrefs.Save();
    }
}
