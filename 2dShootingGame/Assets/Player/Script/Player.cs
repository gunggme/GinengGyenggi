using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class Player : MonoBehaviour
{
    [Header("Player Stat")]
    [SerializeField] float speed;
    [SerializeField] public float hp;
    [SerializeField] public float fuer;

    [SerializeField] int power;

    [SerializeField] float curDelay;
    [SerializeField] float MaxDelay;

    [SerializeField] bool onHit;

    [Header("ETC")]
    [SerializeField] Animator ani;
    [SerializeField] SpriteRenderer spri;
    [SerializeField] ObjectManager objMana;
    [SerializeField] GameManager gameMana;


    private void Awake()
    {
        fuer = 100;
        hp = 40;
        Invoke("FuerDown", 1);

    }

    private void Update()
    {
        Move();
        Shot();
        
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        ani.SetInteger("isMove", (int)h);

        transform.position += new Vector3(h, v, 0) * speed * Time.deltaTime;

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0) pos.x = 0;
        if (pos.x > 1) pos.x = 1;
        if (pos.y < 0) pos.y = 0;
        if (pos.y > 1) pos.y = 1;
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    void Shot()
    {
        if(curDelay < MaxDelay)
        {
            curDelay += Time.deltaTime;
            return;
        }
        if (!Input.GetKey(KeyCode.Z))
        {
            return;
        }

        switch (power)
        {
            case 1:
                GameObject dirC = objMana.MakeObj("PlayerBullet1");
                dirC.transform.position = transform.position;

                Rigidbody2D rigidC = dirC.GetComponent<Rigidbody2D>();
                rigidC.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                break;
            case 2:
                GameObject dirLL = objMana.MakeObj("PlayerBullet1");
                GameObject dirRR = objMana.MakeObj("PlayerBullet1");
                dirLL.transform.position = transform.position + Vector3.left * 0.25f;
                dirRR.transform.position = transform.position + Vector3.right * 0.25f;

                Rigidbody2D rigidLL = dirLL.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidRR = dirRR.GetComponent<Rigidbody2D>();
                rigidLL.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                rigidRR.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                break;
            case 3:
                GameObject dirLLL = objMana.MakeObj("PlayerBullet1");
                GameObject dirCCC = objMana.MakeObj("PlayerBullet2");
                GameObject dirRRR = objMana.MakeObj("PlayerBullet1");
                dirLLL.transform.position = transform.position + Vector3.left * 0.25f;
                dirCCC.transform.position = transform.position;
                dirRRR.transform.position = transform.position + Vector3.right * 0.25f;

                Rigidbody2D rigidLLL = dirLLL.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidCCC = dirCCC.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidRRR = dirRRR.GetComponent<Rigidbody2D>();
                rigidLLL.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                rigidCCC.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                rigidRRR.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                break;
            case 4:
                GameObject dirLLLL = objMana.MakeObj("PlayerBullet2");
                GameObject dirRRRR = objMana.MakeObj("PlayerBullet2");
                dirLLLL.transform.position = transform.position + Vector3.left * 0.25f;
                dirRRRR.transform.position = transform.position + Vector3.right * 0.25f;

                Rigidbody2D rigidLLLL = dirLLLL.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidRRRR = dirRRRR.GetComponent<Rigidbody2D>();
                rigidLLLL.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                rigidRRRR.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                break;
            case 5:
                GameObject dirLLLLL = objMana.MakeObj("PlayerBullet2");
                GameObject dirCCCCC = objMana.MakeObj("PlayerBullet2");
                GameObject dirRRRRR = objMana.MakeObj("PlayerBullet2");
                dirLLLLL.transform.position = transform.position + Vector3.left * 0.35f;
                dirCCCCC.transform.position = transform.position;
                dirRRRRR.transform.position = transform.position + Vector3.right * 0.35f;

                Rigidbody2D rigidLLLLL = dirLLLLL.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidCCCCC = dirCCCCC.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidRRRRR = dirRRRRR.GetComponent<Rigidbody2D>();
                rigidLLLLL.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                rigidCCCCC.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                rigidRRRRR.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                break;
        }
        curDelay = 0;
    }

    void FuerDown()
    {
        fuer -= 5;
        Invoke("FuerDown", 1);
    }

    void OnHit(float dmg)
    {
        hp -= dmg;

        OnHitEffect();
        Invoke("ReturnColor", 1);

        if(hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    void OnHitEffect()
    {
        spri.color = new Color(1, 1, 1, 0.5f);
        onHit = true;
        gameObject.layer = 9;
    }

    void ReturnColor()
    {
        spri.color = new Color(1, 1, 1, 1);
        onHit = false;
        gameObject.layer = 8;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!onHit)
        {
            if (collision.gameObject.CompareTag("EnemyBullet"))
            {
                Bullet bu = collision.gameObject.GetComponent<Bullet>();

                OnHit(bu.dmg);
                collision.gameObject.SetActive(false);
            }

            if (collision.gameObject.CompareTag("Enemy"))
            {
                Enemis enem = collision.gameObject.GetComponent<Enemis>();
                collision.gameObject.SetActive(false);
                OnHit(enem.dmg / 2);
            }
        }

        if (collision.gameObject.CompareTag("Item"))
        {
            Item item = collision.GetComponent<Item>();
            switch (item.name)
            {
                case "Coin":
                    gameMana.score += 300;
                    break;
                case "Power":
                    if (power >= 5)
                    {
                        gameMana.score += 200;
                    }
                    else
                    {
                        power++;
                    }
                    break;
                case "HP":
                    if (hp > 40)
                    {
                        gameMana.score += 200;
                    }
                    else
                    {
                        hp += 10;
                    }
                    break;
                case "Fuer":
                    if (fuer >= 100)
                    {
                        gameMana.score += 200;
                    }
                    else
                    {
                        fuer += 30;
                    }
                    break;
            }
            collision.gameObject.SetActive(false);
        }
    }
}