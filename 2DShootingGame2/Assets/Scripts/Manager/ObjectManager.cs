using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [Header("Item")]
    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject powerPrefab;
    [SerializeField] GameObject hpPrefab;
    [Header("Enemy")]
    [SerializeField] GameObject enemySPrefab; 
    [SerializeField] GameObject enemySsPrefab; 
    [SerializeField] GameObject enemyMPrefab; 
    [SerializeField] GameObject enemyMsPrefab; 
    [SerializeField] GameObject enemyLPrefab;
    [Header("NPC")]
    [SerializeField] GameObject whiteCellPrefab;
    [SerializeField] GameObject redCellPrefab;
    [Header("Enemy Bullet")]
    [SerializeField] GameObject enemyBullet1Prefab;
    [SerializeField] GameObject enemyBullet2Prefab;
    [Header("Boss Bullet")]
    [SerializeField] GameObject bossBullet1Prefab;
    [SerializeField] GameObject bossBullet2Prefab;
    [Header("Player Bullet")]
    [SerializeField] GameObject playerBullet1Prefab;
    [SerializeField] GameObject playerBullet2Prefab;

    //Item
    GameObject[] coin;
    GameObject[] power;
    GameObject[] hp;
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
    //BossBullet
    GameObject[] bossBullet1;
    GameObject[] bossBullet2;
    //playerbullet
    GameObject[] playerBullet1;
    GameObject[] playerBullet2;

    GameObject[] targetPool;

    private void Awake()
    {
        //Item
        coin = new GameObject[10];
        power = new GameObject[10];
        hp = new GameObject[10];
        //Enemy
        enemyS = new GameObject[20];
        enemySs = new GameObject[20];
        enemyM = new GameObject[20];
        enemyMs = new GameObject[20];
        enemyL = new GameObject[20];
        //NPC
        whiteCell = new GameObject[20];
        redCell = new GameObject[20];
        //EnemyBullet
        enemyBullet1 = new GameObject[100];
        enemyBullet2 = new GameObject[100];
        //Boss Bullet
        bossBullet1 = new GameObject[100];
        bossBullet2 = new GameObject[100];
        //PlayerBullet
        playerBullet1 = new GameObject[100];
        playerBullet2 = new GameObject[100];
        
        Generate();
    }

    void Generate()
    {
        //Item
        for(int i = 0; i < coin.Length; i++)
        {
            coin[i] = Instantiate(coinPrefab);
            coin[i].gameObject.SetActive(false);
        }
        for(int i = 0; i < power.Length; i++)
        {
            power[i] = Instantiate(powerPrefab);
            power[i].gameObject.SetActive(false);
        }
        for(int i = 0; i < hp.Length; i++)
        {
            hp[i] = Instantiate(hpPrefab);
            hp[i].gameObject.SetActive(false);
        }
        //Enemy
        for(int i = 0;  i < enemyS.Length; i++)
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
        //NPC
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
        //EnemyBullet
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
        //BossBullet
        for(int i = 0; i < bossBullet1.Length; i++)
        {
            bossBullet1[i] = Instantiate(bossBullet1Prefab);
            bossBullet1[i].gameObject.SetActive(false);
        }
        for(int i = 0; i < bossBullet2.Length; i++)
        {
            bossBullet2[i] = Instantiate(bossBullet2Prefab);
            bossBullet2[i].gameObject.SetActive(false);
        }
        //PlayerBullet
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
    }

    public GameObject MakeObj(string name)
    {
        switch (name)
        {
            //Item
            case "Coin":
                targetPool = coin;
                break;
            case "Power":
                targetPool = power;
                break;
            case "Hp":
                targetPool = hp;
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
            //BossBullet
            case "BossBullet1":
                targetPool = bossBullet1;
                break;
            case "BossBullet2":
                targetPool = bossBullet2;
                break;
            //PlayerBullet
            case "PlayerBullet1":
                targetPool = playerBullet1;
                break;
            case "PlayerBullet2":
                targetPool = playerBullet2;
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
