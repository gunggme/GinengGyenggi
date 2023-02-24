using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemys : MonoBehaviour
{
    [SerializeField] string name;
    [SerializeField] float hp;
    public float speed;
    public float dmg;
    [SerializeField] int score;
    [SerializeField] float curDelay;
    [SerializeField] float maxDelay;

    [Header("Managers")]
    [SerializeField] ObjectManager objMana;

    Transform playerT;
    SpriteRenderer spri;
    GameManager gameMana;

    private void Awake()
    {
        gameMana = GameObject.Find("GameManager").GetComponent<GameManager>();
        spri = GetComponent<SpriteRenderer>();
        playerT = GameObject.Find("Player").GetComponent<Transform>();
        objMana = GameObject.Find("ObjectManager").GetComponent<ObjectManager>();
    }

    private void OnEnable()
    {
        switch (name)
        {
            case "EnemyS":
                hp = 5;
                speed = 3;
                dmg = 2;
                maxDelay = 0.8f;
                score = 300;
                break;
            case "EnemySs":
                hp = 7;
                speed = 5;
                dmg = 4;
                maxDelay = 0.6f;
                score = 350;
                break;
            case "EnemyMs":
                speed = 10; 
                hp = 12;
                dmg = 6;
                maxDelay = 0.7f;
                score = 450;
                break;
            case "EnemyM":
                speed = 8;
                hp = 12;
                dmg = 6;
                score = 400;
                break;
            case "EnemyL":
                hp = 15;
                speed = 1;
                dmg = 8;
                maxDelay = 1f;
                score = 500;
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
        Vector3 PT = playerT.position - transform.position;

        switch (name)
        {
            case "EnemyS":
                GameObject dirS = objMana.MakeObj("EnemyBullet1");
                dirS.transform.position = transform.position;

                Bullets sB = dirS.GetComponent<Bullets>();
                sB.dmg = dmg;

                Rigidbody2D rigidS = dirS.GetComponent<Rigidbody2D>();

                rigidS.AddForce(PT.normalized * 4, ForceMode2D.Impulse);
                break;
            case "EnemySs":
                GameObject dirSs = objMana.MakeObj("EnemyBullet2");
                dirSs.transform.position = transform.position;

                Bullets SsB = dirSs.GetComponent<Bullets>();
                SsB.dmg = dmg;

                Rigidbody2D rigidSs = dirSs.GetComponent<Rigidbody2D>();

                rigidSs.AddForce(PT.normalized * 4, ForceMode2D.Impulse);
                break;
            case "EnemyMs":
                GameObject dirMs = objMana.MakeObj("EnemyBullet2");
                dirMs.transform.position = transform.position;

                Bullets MsB = dirMs.GetComponent<Bullets>();
                MsB.dmg = dmg;

                Rigidbody2D rigidMs = dirMs.GetComponent<Rigidbody2D>();

                rigidMs.AddForce(PT.normalized * 4, ForceMode2D.Impulse);
                break;
            case "EnemyL":
                GameObject dirL = objMana.MakeObj("EnemyBullet2");
                GameObject dirR = objMana.MakeObj("EnemyBullet2");
                dirL.transform.position = transform.position + Vector3.left * 0.25f;
                dirR.transform.position = transform.position + Vector3.right * 0.25f;

                Bullets LB = dirL.GetComponent<Bullets>();
                LB.dmg = dmg;
                Bullets RB = dirR.GetComponent<Bullets>();
                RB.dmg = dmg;

                Rigidbody2D rigidL = dirL.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidR = dirR.GetComponent<Rigidbody2D>();

                rigidL.AddForce(PT.normalized * 4, ForceMode2D.Impulse);
                rigidR.AddForce(PT.normalized * 4, ForceMode2D.Impulse);
                break;
        }
        curDelay = 0;
    }

    public void OnHit(float dmg)
    {
        hp -= dmg;

        OnHitEffect();

        if(hp <= 0)
        {
            transform.rotation = Quaternion.identity;
            gameMana.score += score;
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
            Bullets bullet = collision.GetComponent<Bullets>();
            OnHit(bullet.dmg);
            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Border"))
        {
            transform.rotation = Quaternion.identity;
            gameMana.curSick += 5;
            gameObject.SetActive(false);
        }
    }
}
