using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemis : MonoBehaviour
{
    public string enemName;

    [Header("Stat")]
    [SerializeField] float hp;
    [SerializeField] float curDelay;
    [SerializeField] float maxDelay;
    public float speed;
    [SerializeField] float score;
    
    [Header("ETC")]
    [SerializeField] SpriteRenderer spri;
    [SerializeField] int ranItem;

    string[] itemName;

    Transform playerT;

    private void Awake()
    {
        itemName = new string[] { "Null", "Coin", "Fuer", "Power", "Fuer", "Fuer", "Null", "HP", "Null", "MZ" };
    }

    private void OnEnable()
    {
        switch (enemName)
        {
            case "S":
                hp = 10;
                speed = 3;
                maxDelay = 1.3f;
                score = 300;
                break;
            case "M":
                hp = 13;
                speed = 2;
                maxDelay = 1.1f;
                score = 400;
                break;
            case "L":
                hp = 16;
                speed = 1;
                maxDelay = 1f;
                score = 500;
                break;
            case "Meteor1":
                hp = 5;
                speed = 10;
                maxDelay = 1.3f;
                score = 300;
                break;
            case "Meteor2":
                hp = 8;
                speed = 13;
                maxDelay = 1.3f;
                score = 500;
                break;
        }
    }

    private void Start()
    {
        playerT = GameObject.Find("Player").transform;
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
            GameObject dir = GameManager.instance.objMana.MakeObj("EnemBullet1");
            dir.transform.position = transform.position;
            Vector3 vec = playerT.position - transform.position;
            float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
            dir.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

            dir.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 7, ForceMode2D.Impulse);
        }
        else if (enemName == "M")
        {
            GameObject dir = GameManager.instance.objMana.MakeObj("EnemBullet2");
            dir.transform.position = transform.position;
            Vector3 vec = playerT.position - transform.position;
            float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
            dir.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

            dir.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 7, ForceMode2D.Impulse);
        }
        else if (enemName == "L")
        {
            GameObject dir = GameManager.instance.objMana.MakeObj("EnemBullet3");
            GameObject dir2 = GameManager.instance.objMana.MakeObj("EnemBullet3");
            dir.transform.position = transform.position + Vector3.right * 0.25f;
            dir2.transform.position = transform.position + Vector3.left * 0.25f;
            Vector3 vec = playerT.position - transform.position;
            float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
            dir.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            dir2.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

            dir.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 7, ForceMode2D.Impulse);
            dir2.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 7, ForceMode2D.Impulse);
        }

        curDelay = 0;
    }

    public void OnHit(float dmg)
    {
        hp -= dmg;

        OnHitEffect();

        if(hp < 1)
        {
            ranItem = Random.Range(0, itemName.Length);
            if (itemName[ranItem] != "Null")
            {
                GameObject dir = GameManager.instance.objMana.MakeObj(itemName[ranItem]);
                dir.transform.position = transform.position;
            }
            
            GameManager.instance.score += score;
            gameObject.SetActive(false);
        }
    }
   

    void OnHitEffect()
    {
        spri.color = new Color(1, 1, 1, 0.5f);
        Invoke("ReturnColor", 0.25f);
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
        {
            OnHit(9999);
        }

        if (collision.gameObject.CompareTag("Border"))
            gameObject.SetActive(false);
    }
}
