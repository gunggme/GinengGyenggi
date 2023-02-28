using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Manager")]
    [SerializeField] ObjectManager objMana;

    [Header("SpawnTransform")]
    [SerializeField] Transform[] spawnPo;

    [Header("SpawnDelay")]
    [SerializeField] float curDelay;
    [SerializeField] float nextDelay;

    float speed;
    string[] names;

    private void Awake()
    {
        names = new string[] { "EnemyS", "EnemyS", "EnemySs", "EnemyM", "EnemyMs", "EnemyL", "EnemyS" };
    }

    private void Update()
    {
        SpawnWait();
    }

    void SpawnWait()
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


    void Spawn()
    {
        int dirRandom = Random.Range(0, names.Length - 1);
        int transformRandom = Random.Range(0, spawnPo.Length - 1);

        GameObject dir = objMana.MakeObj(names[dirRandom]);
        dir.transform.position = spawnPo[transformRandom].position;

        Rigidbody2D rigid = dir.GetComponent<Rigidbody2D>();

        if (dir.gameObject.CompareTag("Enemy"))
        {
            Enemys enem = dir.GetComponent<Enemys>();
            speed = enem.speed;
        }

        if(transformRandom == 4 || transformRandom == 5)
        {
            dir.transform.Rotate(Vector3.back * 90);
            rigid.velocity = new Vector2(speed * (-1), -1);
        }
        else if(transformRandom == 6 || transformRandom == 7)
        {
            dir.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector2(speed, -1);
        }
        else
        {
            rigid.velocity = new Vector2(0, speed * -1);
        }
    }
}
