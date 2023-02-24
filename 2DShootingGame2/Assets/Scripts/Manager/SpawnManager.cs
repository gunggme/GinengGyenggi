using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    string[] names;
    [Header("Spawn Points")]
    [SerializeField]
    Transform[] spawnPoints;

    [Header("스폰 딜레이")]
    [SerializeField] float curDelay;
    [SerializeField] float nextDelay;

    //스폰된 오브젝트의 속도
    float speed;

    //Enemy 스폰 제어
    public bool spawnStop = false;

    [Header("스폰매니져")]
    [SerializeField] ObjectManager objMana;


    private void Awake()
    {
        spawnStop = false;
        //소환할 이름을 문자열 배열로 저장    ㅂ
        names = new string[] { "EnemyS", "EnemyS", "WhiteCell", "EnemySs", "EnemyS", "RedCell", "EnemyM", "EnemyMs", "EnemyL", "EnemyS" };
    }

    private void Update()
    {
        SpawnRead();
    }

    void SpawnRead()
    {
        if (!spawnStop)
        {
            if (curDelay < nextDelay)
            {
                curDelay += Time.deltaTime;
                return;
            }
            Spawn();
            nextDelay = Random.Range(0.4f, 1.5f);
            curDelay = 0;
        }

        
    }

    void Spawn()
    {
        int randomEnemy = Random.Range(0, names.Length - 1);
        int randomPoint = Random.Range(0, spawnPoints.Length - 1);

        GameObject dir = objMana.MakeObj(names[randomEnemy]);
        dir.transform.position = spawnPoints[randomPoint].position;

        Rigidbody2D rigid = dir.GetComponent<Rigidbody2D>();

        if (dir.gameObject.CompareTag("Enemy"))
        {
            Enemys enem = dir.GetComponent<Enemys>();
            speed = enem.speed;
        }
        else if (dir.gameObject.CompareTag("WhiteCell"))
        {
            WhiteCell white = dir.GetComponent<WhiteCell>();
            speed = white.speed;
        }
        else if (dir.gameObject.CompareTag("RedCell"))
        {
            RedCell red = dir.GetComponent<RedCell>();
            speed = red.speed;
        }

        if (randomPoint == 5 || randomPoint == 6)
        {
            //각도 설정
            dir.transform.Rotate(Vector3.back * 90);
            rigid.velocity = new Vector2(speed * (-1), -1);
        }
        else if (randomPoint == 7 || randomPoint == 8)
        {
            //각도 설정
            dir.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector2(speed, -1);
        }
        else
        {
            rigid.velocity = new Vector2(0, speed * -1);
        }
        
    }
}
