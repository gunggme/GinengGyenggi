using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheatManager : MonoBehaviour
{
    [SerializeField] Player playerS;
    [SerializeField] GameManager gameMana;

    [SerializeField] GameObject allDel1;

    [SerializeField] Slider playerHPGet;
    [SerializeField] Slider sickSet;

    private void Update()
    {
        PlayerCheat();
        StageMove();
        SetHPorSick();

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            AllDel();   
        }
    }

    void PlayerCheat()
    {
        //플레이어 강화 변경
        //+1
        if (Input.GetKeyDown(KeyCode.V))
        {
            playerS.power++;
        }
        //-1
        if (Input.GetKeyDown(KeyCode.B))
        {
            playerS.power--;
        }

        //플레이어 무적
        //ON
        if (Input.GetKeyDown(KeyCode.T))
        {
            playerS.OnHitEffect();
        }
        //OFF
        if (Input.GetKeyDown(KeyCode.Y))
        {
            playerS.ReturnColor();
        }
    }

    //스테이지 이동
    void StageMove()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            SceneManager.LoadScene(1);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            gameMana.StageEnd();
        }
    }

    bool isOn;

    void SetHPorSick()
    {
        if (Input.GetKey(KeyCode.Slash))
        {
            playerHPGet.gameObject.SetActive(true);
            sickSet.gameObject.SetActive(true);
            playerS.hp = playerHPGet.value * 30;
            gameMana.curSick = sickSet.value * 100;
        }
        else
        {
            playerHPGet.gameObject.SetActive(false);
            sickSet.gameObject.SetActive(false);
        }
    }

    //모든 적 삭제
    void AllDel()
    {
        allDel1.gameObject.SetActive(true);
        Invoke("Off", 0.3f);
    }

    void Off()
    {
        allDel1.gameObject.SetActive(false);
    }
}
