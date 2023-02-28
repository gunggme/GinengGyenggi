using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] GameObject enemySPrefab;
    [SerializeField] GameObject enemySsPrefab;
    [SerializeField] GameObject enemyMPrefab;
    [SerializeField] GameObject enemyMsPrefab;
    [SerializeField] GameObject enemyLPrefab;
    [Header("Enemy Bullet")]
    [SerializeField] GameObject enemyBullet1Prefab;
    [SerializeField] GameObject enemyBullet2Prefab;
    [Header("Player Bullet")]
    [SerializeField] GameObject playerBullet1Prefab;
    [SerializeField] GameObject playerBullet2Prefab;

    //Enemy
    GameObject[] enemyS;
    GameObject[] enemySs;
    GameObject[] enemyM;
    GameObject[] enemyMs;
    GameObject[] enemyL;
    //EnemyBullet
    GameObject[] enemyBullet1;
    GameObject[] enemyBullet2;
    //PlayerBullet
    GameObject[] playerBullet1;
    GameObject[] playerBullet2;

    GameObject[] targetPool;

    private void Awake()
    {
        //Enemy
        enemyS = new GameObject[20];
        enemySs = new GameObject[20];
        enemyM = new GameObject[20];
        enemyMs = new GameObject[20];
        enemyL = new GameObject[20];
        //EnemyBullet
        enemyBullet1 = new GameObject[100];
        enemyBullet2 = new GameObject[100];
        //PlayerBullet
        playerBullet1 = new GameObject[100];
        playerBullet2 = new GameObject[100];

        Generate();
    }

    void Generate()
    {
        //Enemy
        for(int i = 0; i < enemyS.Length; i++)
        {
            enemyS[i] = Instantiate(enemySPrefab);
            enemyS[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < enemySs.Length; i++)
        {
            enemySs[i] = Instantiate(enemySsPrefab);
            enemySs[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < enemyM.Length; i++)
        {
            enemyM[i] = Instantiate(enemyMPrefab);
            enemyM[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < enemyMs.Length; i++)
        {
            enemyMs[i] = Instantiate(enemyMsPrefab);
            enemyMs[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < enemyL.Length; i++)
        {
            enemyL[i] = Instantiate(enemyLPrefab);
            enemyL[i].gameObject.SetActive(false);
        }
        //EnemyBullet
        for (int i = 0; i < enemyBullet1.Length; i++)
        {
            enemyBullet1[i] = Instantiate(enemyBullet1Prefab);
            enemyBullet1[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < enemyBullet2.Length; i++)
        {
            enemyBullet2[i] = Instantiate(enemyBullet2Prefab);
            enemyBullet2[i].gameObject.SetActive(false);
        }
        //PlayerBullet
        for (int i = 0; i < playerBullet1.Length; i++)
        {
            playerBullet1[i] = Instantiate(playerBullet1Prefab);
            playerBullet1[i].gameObject.SetActive(false);
        }
        for(int i = 0; i < playerBullet2.Length; i++)
        {
            playerBullet2[i] = Instantiate(playerBullet2Prefab);
            playerBullet2[i].gameObject.SetActive(false);
        }
    }

    public GameObject MakeObj(string name)
    {
        switch (name)
        {
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
            //PlayerBullet
            case "PlayerBullet1":
                targetPool = playerBullet1;
                break;
            case "PlayerBullet2":
                targetPool = playerBullet2;
                break;
            //EnemyBullet
            case "EnemyBullet1":
                targetPool = enemyBullet1;
                break;
            case "EnemyBullet2":
                targetPool = enemyBullet2;
                break;
        }

        for(int i = 0; i < targetPool.Length; i++)
        {
            if (!targetPool[i].activeSelf)
            {
                targetPool[i].gameObject.SetActive(true);
                return targetPool[i];
            }
        }
        return null;
    }
}
