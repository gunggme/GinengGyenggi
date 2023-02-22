using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] Transform[] spawnTransform;

    [SerializeField] ObjectManager objMana;

    [SerializeField] float curDelay;
    [SerializeField] float nextDelay;
    [SerializeField] float speed;

    [SerializeField] string[] names;

    public bool bossOn;

    private void Awake()
    {
        names = new string[] { "EnemyS", "EnemyS", "RedCell", "EnemySs", "WhiteCell", "EnemyS", "EnemyM", "WhiteCell", "EnemyL"};
    }

    private void Update()
    {
        SpawnReady();
        SpawnCheat();
    }

    void SpawnReady()
    {
        if (!bossOn)
        {
            if (curDelay < nextDelay)
            {
                curDelay += Time.deltaTime;
                return;
            }

            nextDelay = Random.Range(0.5f, 1.5f);
            Spawn();
            curDelay = 0;
        }
    }

    void Spawn()
    {
        int randomName = Random.Range(0, names.Length - 1);
        int randomTransfrom = Random.Range(0, spawnTransform.Length - 1);
        GameObject obj = objMana.MakeObj(names[randomName]);
        obj.transform.position = spawnTransform[randomTransfrom].position;
        Rigidbody2D rigid = obj.GetComponent<Rigidbody2D>();

        if (obj.CompareTag("Enemy"))
        {
            Enemys enemy = obj.GetComponent<Enemys>();
            speed = enemy.speed;
        }
        if (obj.CompareTag("NPC"))
        {
            if(obj.name == "WhiteCell(Clone)")
            {
                WhiteCell white = obj.GetComponent<WhiteCell>();
                speed = white.speed;
            }
            if(obj.name == "RedCell(Clone)")
            {
                RedCell red = obj.GetComponent<RedCell>();
                speed = red.speed;
            }
        }

        if(randomTransfrom == 5 || randomTransfrom == 6)
        {
            obj.transform.Rotate(Vector3.back * 90);
            rigid.velocity = new Vector2(speed * (-1), -1);
        }
        else if(randomTransfrom == 7 || randomTransfrom == 8)
        {
            obj.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector2(speed, -1);
        }
        else
        {
            rigid.velocity = new Vector2(0, speed * (-1));
        }

    }

    void SpawnCheat()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            int randomTransfrom = Random.Range(0, spawnTransform.Length - 1);
            GameObject obj = objMana.MakeObj(names[4]);
            obj.transform.position = spawnTransform[randomTransfrom].position;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            int randomTransfrom = Random.Range(0, spawnTransform.Length - 1);
            GameObject obj = objMana.MakeObj(names[2]);
            obj.transform.position = spawnTransform[randomTransfrom].position;
        }
    }
}
