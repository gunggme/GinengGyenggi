using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoint;

    [SerializeField] float curDelay;
    [SerializeField] float nextDelay;

    public bool isSpawn = true;

    [SerializeField] string[] enemNames;

    int ranTran;
    int ranEnem;

    private void Awake()
    {
        enemNames = new string[] { "EnemS", "EnemM", "EnemL", "Meteor1", "Meteor2" };
    }

    private void Update()
    {
        SpawnWait();
    }

    void SpawnWait()
    {
        if (isSpawn)
        {
            if(curDelay < nextDelay)
            {
                curDelay += Time.deltaTime;
                return;
            }

            Spawn();
            curDelay = 0;
            nextDelay = Random.Range(0.5f, 1.5f);
        }
    }

    void Spawn()
    {
        if(GameManager.instance.stageNum == 1)
        {
            ranTran = Random.Range(0, spawnPoint.Length - 4);
        }
        else if(GameManager.instance.stageNum == 2)
        {
            ranTran = Random.Range(0, spawnPoint.Length);
        }

        ranEnem = Random.Range(0, enemNames.Length);

        GameObject dir = GameManager.instance.objMana.MakeObj(enemNames[ranEnem]);
        dir.transform.position = spawnPoint[ranTran].position;

        Rigidbody2D rigid = dir.GetComponent<Rigidbody2D>();
        Enem enem = dir.GetComponent<Enem>();

        if(ranTran == 5 || ranTran == 6)
        {
            dir.transform.Rotate(Vector3.back * 90);
            rigid.velocity = new Vector2(-enem.speed, -1);
        }
        else if(ranTran == 7 || ranTran == 8)
        {
            dir.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector2(enem.speed, -1);
        }
        else
        {
            rigid.velocity = new Vector2(0, -enem.speed);
        }
    }
}
