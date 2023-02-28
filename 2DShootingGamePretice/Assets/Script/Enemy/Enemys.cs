using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemys : MonoBehaviour
{
    [Header("EnemyName")]
    [SerializeField] string name;

    [Header("EnemyStet")]
    float hp;
    public float dmg;
    public float speed;
    float curDelay;
    float maxDelay;

    int Score;

    ObjectManager objMana;
    Transform playerT;
    GameManager gameMana;
    SpriteRenderer spri;

    private void Awake()
    {
        spri = GetComponent<SpriteRenderer>();
        gameMana = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerT = GameObject.Find("Player").transform;
        objMana = GameObject.Find("ObjectManager").GetComponent<ObjectManager>();
    }

    private void OnEnable()
    {
        switch (name)
        {
            case "EnemyS":
                hp = 5;
                dmg = 4;
                speed = 3;
                maxDelay = 1;
                Score = 300;
                break;
            case "EnemySs":
                hp = 5;
                dmg = 4;
                speed = 5;
                Score = 500;
                maxDelay = 0.8f;
                break;
            case "EnemyM":
                hp = 7;
                dmg = 6;
                Score = 600;
                speed = 10;
                break;
            case "EnemyMs":
                hp = 7;
                dmg = 6;
                speed = 8;
                Score = 900;
                maxDelay = 0.7f;
                break;
            case "EnemyL":
                hp = 15;
                dmg = 8;
                speed = 1;
                Score = 1000;
                maxDelay = 1;
                break;
        }
    }

    private void Update()
    {
        Shot();
    }

    void Shot()
    {
        if(curDelay < maxDelay)
        {
            curDelay += Time.deltaTime;
            return;
        }

        if(name == "EnemyS")
        {
            GameObject dir = objMana.MakeObj("EnemyBullet1");
            dir.transform.position = transform.position;

            Vector3 pos = playerT.position - dir.transform.position;

            Bullet b = dir.GetComponent<Bullet>();
            b.dmg = dmg;

            Rigidbody2D rigid = dir.gameObject.GetComponent<Rigidbody2D>();
            rigid.AddForce(pos.normalized * 5, ForceMode2D.Impulse);
        }
        else if (name == "EnemySs")
        {
            GameObject dir = objMana.MakeObj("EnemyBullet2");
            dir.transform.position = transform.position;

            Vector3 pos = playerT.position - dir.transform.position;

            Bullet b = dir.GetComponent<Bullet>();
            b.dmg = dmg;

            Rigidbody2D rigid = dir.gameObject.GetComponent<Rigidbody2D>();
            rigid.AddForce(pos.normalized * 5, ForceMode2D.Impulse);
        }
        else if (name == "EnemyMs")
        {
            GameObject dir = objMana.MakeObj("EnemyBullet2");
            dir.transform.position = transform.position;

            Vector3 pos = playerT.position - dir.transform.position;

            Bullet b = dir.GetComponent<Bullet>();
            b.dmg = dmg;

            Rigidbody2D rigid = dir.gameObject.GetComponent<Rigidbody2D>();
            rigid.AddForce(pos.normalized * 6, ForceMode2D.Impulse);
        }
        else if (name == "EnemyL")
        {
            GameObject dir = objMana.MakeObj("EnemyBullet2");
            GameObject dir2 = objMana.MakeObj("EnemyBullet2");
            dir.transform.position = transform.position;
            dir2.transform.position = transform.position;

            Vector3 pos = playerT.position - dir.transform.position;

            Bullet b = dir.GetComponent<Bullet>();
            Bullet b2 = dir2.GetComponent<Bullet>();
            b.dmg = dmg;
            b2.dmg = dmg;

            Rigidbody2D rigid = dir.gameObject.GetComponent<Rigidbody2D>();
            Rigidbody2D rigid2 = dir2.gameObject.GetComponent<Rigidbody2D>();
            rigid.AddForce(pos.normalized * 5, ForceMode2D.Impulse);
            rigid2.AddForce(pos.normalized * 5, ForceMode2D.Impulse);
        }

        curDelay = 0;
    }

    public void OnHit(float dmg)
    {
        hp -= dmg;

        OnHitEffect();   

        if(hp < 0)
        {
            gameMana.score += Score;
            transform.rotation = Quaternion.identity;
            gameObject.SetActive(false);
        }
    }

    void OnHitEffect()
    {
        spri.color = new Color(1, 1, 1, 0.4f);
        Invoke("ReturnColor", 0.3f);
    }

    void ReturnColor()
    {
        spri.color = new Color(1, 1, 1, 1);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Bullet pb = collision.gameObject.GetComponent<Bullet>();
            OnHit(pb.dmg);
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Border"))
        {
            gameMana.sick += 5;
            transform.rotation = Quaternion.identity;
            gameObject.SetActive(false);
        }
    }
}
