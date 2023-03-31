using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemis : MonoBehaviour
{
    [SerializeField] string enemName;

    [Header("Stat")]
    [SerializeField] float hp;
    public float speed;
    [SerializeField] float score;

    [Header("Delay")]
    [SerializeField] float curDelay;
    [SerializeField] float maxDelay;

    [Header("ETC")]
    [SerializeField] SpriteRenderer spri;
    [SerializeField] Transform playerT;

    private void Awake()
    {
        playerT = GameObject.Find("Player").transform;
    }

    private void OnEnable()
    {
        switch (enemName)
        {
            case "S":
                hp = 8;
                speed = 3;
                score = 300;
                maxDelay = 1.3f;
                break;
            case "M":
                hp = 14;
                speed = 2;
                score = 500;
                maxDelay = 1.2f;
                break;
            case "L":
                hp = 17;
                speed = 1;
                score = 900;
                maxDelay = 0.8f;
                break;
            case "Meteor1":
                hp = 6;
                speed = 8;
                score = 300;
                break;
            case "Meteor2":
                hp = 10;
                speed = 10;
                score = 700;
                break;
        }
    }

    void Update()
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
            GameObject dir = GameManager.instance.objMana.MakeObj("EnemBullet1");
            dir.transform.position = transform.position;
            float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
            dir.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

            dir.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 7, ForceMode2D.Impulse);
        }
        else if (enemName == "M")
        {
            Vector3 vec = playerT.position - transform.position;
            GameObject dir = GameManager.instance.objMana.MakeObj("EnemBullet2");
            dir.transform.position = transform.position;
            float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
            dir.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

            dir.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 7, ForceMode2D.Impulse);
        }
        else if (enemName == "L")
        {
            Vector3 vec = playerT.position - transform.position;
            GameObject dir = GameManager.instance.objMana.MakeObj("EnemBullet3");
            GameObject dir2 = GameManager.instance.objMana.MakeObj("EnemBullet3");
            dir.transform.position = transform.position + Vector3.right * 0.25f;
            dir2.transform.position = transform.position + Vector3.left * 0.25f;
            float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
            dir.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            dir2.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

            dir.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 7, ForceMode2D.Impulse);
            dir2.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 7, ForceMode2D.Impulse);
        }

        curDelay = 0;
    }

    void OnHit(float dmg)
    {
        hp -= dmg;

        OnHitEffect();

        if(hp < 1)
        {
            GameManager.instance.score += score;
            gameObject.transform.rotation = Quaternion .identity;
            gameObject.SetActive(false);
        }
    }

    void OnHitEffect()
    {
        spri.color = new Color(1, 1, 1, 0.5f);
        Invoke("ReturnColor", 0.2f);
    }

    void ReturnColor()
    {
        spri.color = new Color(1, 1, 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Bullet bu = collision.gameObject.GetComponent<Bullet>();
            OnHit(bu.dmg);
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Player"))
            OnHit(9999);

        if (collision.gameObject.CompareTag("Border"))
        {
            gameObject.transform.rotation = Quaternion.identity;
            gameObject.SetActive(false);
        }
    }
}
