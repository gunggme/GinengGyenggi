using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    //Items Prefab
    [SerializeField] GameObject PowerPrefab;
    [SerializeField] GameObject CoinPrefab;
    [SerializeField] GameObject HPPrefab;
    [SerializeField] GameObject MZItemPrefab;
    [SerializeField] GameObject sickDownPrefab;
    [SerializeField] GameObject boomPrefab;
    //Enemys Prefab
    [SerializeField] GameObject enemySPrefab;
    [SerializeField] GameObject enemySsPrefab;
    [SerializeField] GameObject enemyMPrefab;
    [SerializeField] GameObject enemyMsPrefab;
    [SerializeField] GameObject enemyLPrefab;
    //NPC
    [SerializeField] GameObject whiteCellPrefab;
    [SerializeField] GameObject redCellPrefab;
    //EnemyBullet Prefab
    [SerializeField] GameObject enemyBullet1Prefab;
    [SerializeField] GameObject enemyBullet2Prefab;
    //PlayerBullet Prefab
    [SerializeField] GameObject playerBullet1Prefab;
    [SerializeField] GameObject playerBullet2Prefab;
    //BossBullet Prefab
    [SerializeField] GameObject bossBullet1Prefab;
    [SerializeField] GameObject bossBullet2Prefab;

    //Item
    GameObject[] Coin;
    GameObject[] HP;
    GameObject[] Power;
    GameObject[] MZItem;
    GameObject[] sickDown;
    GameObject[] boom;
    //Enemy
    GameObject[] enemyS;
    GameObject[] enemySs;
    GameObject[] enemyM;
    GameObject[] enemyMs;
    GameObject[] enemyL;
    //NPC
    GameObject[] whiteCell;
    GameObject[] redCell;
    //EnemyBullet
    GameObject[] enemyBullet1;
    GameObject[] enemyBullet2;
    //PlayerBullet
    GameObject[] playerBullet1;
    GameObject[] playerBullet2;
    //BossBullet
    GameObject[] bossBullet1;
    GameObject[] bossBullet2;

    GameObject[] targetPool;


    //초기화
    private void Awake()
    {
        //Item 배열 초기화
        Power = new GameObject[10];
        Coin = new GameObject[10];
        HP = new GameObject[10];
        MZItem = new GameObject[10];
        sickDown = new GameObject[10];
        boom = new GameObject[10];
        //Enemy 배열 초기화
        enemyS = new GameObject[20];
        enemySs = new GameObject[20];
        enemyM = new GameObject[20];
        enemyMs = new GameObject[20];
        enemyL = new GameObject[20];
        //NPC 배열 초기화
        whiteCell = new GameObject[20];
        redCell = new GameObject[20];
        //EnemyBullet 배열 초기화
        enemyBullet1 = new GameObject[100];
        enemyBullet2 = new GameObject[100];
        //PlayerBullet 배열 초기화
        playerBullet1 = new GameObject[100];
        playerBullet2 = new GameObject[100];
        //BossBullet
        bossBullet1 = new GameObject[100];
        bossBullet2 = new GameObject[100];


        Generate();
    }

    //프리팹 생성
    void Generate()
    {
        //Item 배열 크기에 맞게 프리팹 생성
        for(int i = 0; i< Power.Length; i++)
        {
            Power[i] = Instantiate(PowerPrefab);
            Power[i].gameObject.SetActive(false);
        }
        for(int i =0; i< Coin.Length; i++)
        {
            Coin[i] = Instantiate(CoinPrefab);
            Coin[i].gameObject.SetActive(false);
        }
        for(int i = 0; i< HP.Length; i++)
        {
            HP[i] = Instantiate(HPPrefab);
            HP[i].gameObject.SetActive(false);
        }
        for(int i = 0; i < MZItem.Length; i++)
        {
            MZItem[i] = Instantiate(MZItemPrefab);
            MZItem[i].gameObject.SetActive(false);
        }
        for(int i = 0; i < sickDown.Length; i++)
        {
            sickDown[i] = Instantiate(sickDownPrefab);
            sickDown[i].gameObject.SetActive(false);
        }
        for(int i = 0; i < boom.Length; i++)
        {
            boom[i] = Instantiate(boomPrefab);
            boom[i].gameObject.SetActive(false);
        }
        //Enemy 배열 크기에 맞게 프리팹 생성
        for(int i = 0; i < enemyS.Length; i++)
        {
            enemyS[i] = Instantiate(enemySPrefab);
            enemyS[i].gameObject.SetActive(false);
        }
        for(int i = 0; i < enemySs.Length; i++)
        {
            enemySs[i] = Instantiate(enemySsPrefab);
            enemySs[i].gameObject.SetActive(false);
        }
        for(int i = 0; i < enemyM.Length; i++)
        {
            enemyM[i] = Instantiate(enemyMPrefab);
            enemyM[i].gameObject.SetActive(false);
        }
        for(int i = 0; i < enemyMs.Length; i++)
        {
            enemyMs[i] = Instantiate(enemyMsPrefab);
            enemyMs[i].gameObject.SetActive(false);
        }
        for(int i = 0; i < enemyL.Length; i++)
        {
            enemyL[i] = Instantiate(enemyLPrefab);
            enemyL[i].gameObject.SetActive(false);
        }
        //NPC 배열 크기에 맞게 프리팹 생성
        for(int i = 0; i < whiteCell.Length; i++)
        {
            whiteCell[i] = Instantiate(whiteCellPrefab);
            whiteCell[i].gameObject.SetActive(false);
        }
        for(int i = 0; i < redCell.Length; i++)
        {
            redCell[i] = Instantiate(redCellPrefab);
            redCell[i].gameObject.SetActive(false);
        }
        //EnemyBullet 배열 크기에 맞게 프리팹 생성
        for(int i = 0; i < enemyBullet1.Length; i++)
        {
            enemyBullet1[i] = Instantiate(enemyBullet1Prefab);
            enemyBullet1[i].gameObject.SetActive(false);
        }
        for(int i = 0; i < enemyBullet2.Length; i++)
        {
            enemyBullet2[i] = Instantiate(enemyBullet2Prefab);
            enemyBullet2[i].gameObject.SetActive(false);
        }
        //PlayerBullet 배열 크기에 맞게 프리팹 생성
        for(int i = 0; i < playerBullet1.Length; i++)
        {
            playerBullet1[i] = Instantiate(playerBullet1Prefab);
            playerBullet1[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < playerBullet2.Length; i++)
        {
            playerBullet2[i] = Instantiate(playerBullet2Prefab);
            playerBullet2[i].gameObject.SetActive(false);
        }
        //BossBullet 배열 크기에 맞게 프리팹 생성
        for(int i = 0; i < bossBullet1.Length; i++)
        {
            bossBullet1[i] = Instantiate(bossBullet1Prefab);
            bossBullet1[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < bossBullet2.Length; i++)
        {
            bossBullet2[i] = Instantiate(bossBullet2Prefab);
            bossBullet2[i].gameObject.SetActive(false);
        }
    }

    //프리팹 사용
    public GameObject MakeObj(string name)
    {
        //switch~ case문으로 오브젝트 풀링
        switch (name)
        {
            //Item
            case "Coin":
                targetPool = Coin;
                break;
            case "Power":
                targetPool = Power;
                break;
            case "HP":
                targetPool = HP;
                break;
            case "MZItem":
                targetPool = MZItem;
                break;
            case "SickDown":
                targetPool = sickDown;
                break;
            case "Boom":
                targetPool = boom;
                break;
            //Enemy
            case "EnemyS":
                targetPool = enemyS;
                break;
            case "EnemySs":
                targetPool = enemySs;
                break;
            case "EnemyM":
                targetPool = enemyM;
                break;
            case "EnemyMs":
                targetPool = enemyMs;
                break;
            case "EnemyL":
                targetPool = enemyL;
                break;
            //NPC
            case "WhiteCell":
                targetPool = whiteCell; 
                break;
            case "RedCell":
                targetPool = redCell;
                break;
            //EnemyBullet
            case "EnemyBullet1":
                targetPool = enemyBullet1;
                break;
            case "EnemyBullet2":
                targetPool = enemyBullet2;
                break;
            //PlayerBullet
            case "PlayerBullet1":
                targetPool = playerBullet1;
                break;
            case "PlayerBullet2":
                targetPool = playerBullet2;
                break;
            //BossBullet
            case "BossBullet1":
                targetPool = bossBullet1;
                break;
            case "BossBullet2":
                targetPool = bossBullet2;
                break;
        }

        for(int i = 0; i < targetPool.Length; i++)
        {
            if (!targetPool[i].activeSelf)
            {
                targetPool[i].SetActive(true);
                return targetPool[i];
            }
        }
        return null;
    }
}
