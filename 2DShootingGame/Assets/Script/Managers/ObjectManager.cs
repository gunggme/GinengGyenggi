using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    //Enemys Prefab
    [SerializeField] GameObject enemySPrefab;
    [SerializeField] GameObject enemySsPrefab;
    [SerializeField] GameObject enemyMPrefab;
    [SerializeField] GameObject enemyLPrefab;
    //EnemyBullet Prefab
    [SerializeField] GameObject enemyBullet1Prefab;
    [SerializeField] GameObject enemyBullet2Prefab;
    //PlayerBullet Prefab
    [SerializeField] GameObject playerBullet1Prefab;
    [SerializeField] GameObject playerBullet2Prefab;

    //Enemy
    GameObject[] enemyS;
    GameObject[] enemySs;
    GameObject[] enemyM;
    GameObject[] enemyL;
    //EnemyBullet
    GameObject[] enemyBullet1;
    GameObject[] enemyBullet2;
    //PlayerBullet
    GameObject[] playerBullet1;
    GameObject[] playerBullet2;

    GameObject[] targetPool;


    //초기화
    private void Awake()
    {
        //Enemy 배열 초기화
        enemyS = new GameObject[20];
        enemySs = new GameObject[20];
        enemyM = new GameObject[20];
        enemyL = new GameObject[20];
        //EnemyBullet 배열 초기화
        enemyBullet1 = new GameObject[100];
        enemyBullet2 = new GameObject[100];
        //PlayerBullet 배열 초기화
        playerBullet1 = new GameObject[100];
        playerBullet2 = new GameObject[100];

        Generate();
    }

    //프리팹 생성
    void Generate()
    {
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
        for(int i = 0; i < enemyL.Length; i++)
        {
            enemyL[i] = Instantiate(enemyLPrefab);
            enemyL[i].gameObject.SetActive(false);
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
    }

    //프리팹 사용
    public GameObject MakeObj(string name)
    {
        //switch~ case문으로 오브젝트 풀링
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
            case "EnemyL":
                targetPool = enemyL;
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
