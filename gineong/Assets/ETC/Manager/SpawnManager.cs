using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] string[] names;

    [SerializeField] Transform[] firePosition;

    [SerializeField] float curDelay;
    [SerializeField] float nextDelay;

    [SerializeField] int ranTran;

    public bool isSpawn;

    private void Awake()
    {
        names = new string[] { "Meteor1", "Meteor2", "EnemS", "EnemS", "EnemM", "EnemL", "Meteor1", "Meteor1", "Meteor2", "EnemS" };
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
            nextDelay = Random.Range(0.8f, 1.2f);
        }
    }

    void Spawn()
    {
        switch (GameManager.instance.stageNum)
        {
            case 1:
                ranTran = Random.Range(0, names.Length - 4);
                break;
            case 2:
                ranTran = Random.Range(0, names.Length);
                break;
        }
        int ranEnem = Random.Range(0, names.Length);

        GameObject dir = GameManager.instance.objMana.MakeObj(names[ranEnem]);
        dir.transform.position = firePosition[ranTran].position;

        Rigidbody2D rigid = dir.GetComponent<Rigidbody2D>();
        Enemis logic = dir.GetComponent<Enemis>();

        if(ranTran == 5 || ranTran == 6)
        {
            dir.transform.Rotate(Vector3.back * 90);
            rigid.velocity = new Vector2(logic.speed * -1, -1);
        }
        else if(ranTran == 7 || ranTran == 8)
        {
            dir.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector2(logic.speed, -1);
        }
        else
        {
            rigid.velocity = new Vector2(0, logic.speed * -1);
        }
    }
}
