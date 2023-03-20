using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemis : MonoBehaviour
{
    [SerializeField] string enemName;

    [Header("Enem Stat")]
    [SerializeField] float hp;
    [SerializeField] public float speed;

    [Header("EnemDelay")]
    [SerializeField] float curDelay;
    [SerializeField] float maxDelay;

    [Header("ETC")]
    [SerializeField] SpriteRenderer spri;
    [SerializeField] Transform playerT;
    [SerializeField] float score;

    private void Awake()
    {
        playerT = GameObject.Find("Player").transform;
    }

    private void OnEnable()
    {
        switch(enemName)
        {
            case "S":
                hp = 10;
                speed = 4;
                maxDelay = 1;
                score = 300;
                break;
            case "M":
                hp = 14;
                maxDelay = 0.8f;
                speed = 3;
                score = 400;
                break;
            case "L":
                hp = 16;
                speed = 1;
                maxDelay = 1;
                score = 600;
                break;
            case "Meteo1":
                hp = 6;
                speed = 6;
                score = 200;
                break;
            case "Meteo2":
                hp = 8;
                speed = 8;
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

        if(enemName == "S")
        {
            Vector3 vec = playerT.position - transform.position;
            GameObject dir = GameManager.Instance.objMana.MakeObj("EnemBullet1");
            dir.transform.position = transform.position;
            float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
            dir.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

            Rigidbody2D rigid = dir.GetComponent<Rigidbody2D>();
            rigid.AddForce(vec.normalized * 6, ForceMode2D.Impulse);
        }
        else if(enemName == "M")
        {
            Vector3 vec = playerT.position - transform.position;
            GameObject dir = GameManager.Instance.objMana.MakeObj("EnemBullet2");
            dir.transform.position = transform.position;
            float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
            dir.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

            Rigidbody2D rigid = dir.GetComponent<Rigidbody2D>();
            rigid.AddForce(vec.normalized * 6, ForceMode2D.Impulse);
        }
        else if(enemName == "L")
        {
            Vector3 vec = playerT.position - transform.position;
            GameObject dir = GameManager.Instance.objMana.MakeObj("EnemBullet3");
            GameObject dir2 = GameManager.Instance.objMana.MakeObj("EnemBullet3");
            dir.transform.position = transform.position;
            dir2.transform.position = transform.position;
            float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
            dir.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            dir2.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

            Rigidbody2D rigid = dir.GetComponent<Rigidbody2D>();
            Rigidbody2D rigid2 = dir2.GetComponent<Rigidbody2D>();
            rigid.AddForce(vec.normalized * 7, ForceMode2D.Impulse);
            rigid2.AddForce(vec.normalized * 7, ForceMode2D.Impulse);
        }

        curDelay = 0;
    }


    void OnHit(float dmg)
    {
        hp -= dmg;

        OnHitEffect();

        if(hp < 1)
        {
            transform.position = new Vector2(10, 8);
            GameManager.Instance.score += score;
            GameManager.Instance.SetScore();
            gameObject.SetActive(false);
        }
    }

    void OnHitEffect()
    {
        spri.color = new Color(1, 1, 1, 0.7f);
        Invoke("ReturnColor", 0.3f);
    }

    void ReturnColor()
    {
        spri.color = new Color(1, 1, 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
            gameObject.SetActive(false);
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Bullet bu = collision.gameObject.GetComponent<Bullet>();
            OnHit(bu.dmg);
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Player"))
            OnHit(9999);
    }
}
