using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] Transform[] spawnTran;

    string[] enemName;

    [SerializeField] float curDelay;
    [SerializeField] float maxDelay = 1;

    [SerializeField] int ranTran;
    [SerializeField] int ranEnem;

    public bool isSpawn;

    private void Awake()
    {
        isSpawn = true;
        enemName = new string[] { "EnemS", "EnemM", "EnemL", "Meteo1", "Meteo2" };
    }

    private void Update()
    {
        SpawnWait();
    }

    void SpawnWait()
    {
        if (isSpawn)
        {
            if (curDelay < maxDelay)
            {
                curDelay += Time.deltaTime;
                return;
            }

            curDelay = 0;
            maxDelay = Random.Range(0.7f, 1.3f);
            Spawn();
        }
    }

    void Spawn()
    {
        switch (GameManager.instance.stageNum)
        {
            case 1:
                ranTran = Random.Range(0, spawnTran.Length - 4);
                ranEnem = Random.Range(0, enemName.Length);
                break;
            case 2:
                ranTran = Random.Range(0, spawnTran.Length );
                ranEnem = Random.Range(0, enemName.Length);
                break;
        }

        GameObject dir = GameManager.instance.objMana.MakeObj(enemName[ranEnem]);
        dir.transform.position = spawnTran[ranTran].position;

        Rigidbody2D rigid = dir.GetComponent<Rigidbody2D>();

        Enemis enem = dir.GetComponent<Enemis>();

        if(ranTran == 5 || ranTran == 6)
        {
            dir.transform.Rotate(Vector3.back * 90);
            rigid.velocity = new Vector2(enem.speed * -1, -1);
        }
        else if (ranTran == 7 || ranTran == 8)
        {
            dir.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector2(enem.speed, -1);
        }
        else
        {
            rigid.velocity = new Vector2(0, enem.speed * -1);
        }
    }
}
