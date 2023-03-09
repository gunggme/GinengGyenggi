using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player stats")]
    [SerializeField] float speed;
    [SerializeField] public float hp;
    [SerializeField] public float oil;
    [SerializeField] float power;

    [Header("ShotDelay")]
    [SerializeField] float curDelay;
    [SerializeField] float maxDelay;

    [Header("Skills")]
    [SerializeField] float curSkill1Delay;
    [SerializeField] float curSkill2Delay;
    [SerializeField] float skill1Delay;
    [SerializeField] float skill2Delay;

    [Header("OilDownDelay")]
    [SerializeField] float curOilDownDelay;
    [SerializeField] float maxOilDownDelay;

    [SerializeField] bool isHit;

    [Header("BoomObj")]
    [SerializeField] GameObject boom;

    [Header("ETC")]
    [SerializeField] SpriteRenderer spri;
    [SerializeField] Animator playerA;
    [SerializeField] ObjManager objMana;
    [SerializeField] GameManager gameMana;

    private void Awake()
    {
        oil = 100;
        hp = 30;
        maxDelay = 0.3f;
    }

    private void Update()
    {
        Move();
        Shot();
        Skill1();
        Skill2();
        oilDown();

        if (hp > 30)
        {
            hp = 30;
        }
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        playerA.SetInteger("isMove", (int)h);

        transform.position += new Vector3(h, v, 0) * speed * Time.deltaTime;

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if(pos.x < 0) pos.x = 0;
        if(pos.x > 1) pos.x = 1;
        if(pos.y < 0) pos.y = 0;
        if(pos.y > 1) pos.y = 1;
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    void Shot()
    {
        if(curDelay < maxDelay)
        {
            curDelay += Time.deltaTime;
            return;
        }

        if (!Input.GetKey(KeyCode.Z))
            return;

        switch (power)
        {
            case 1:
                GameObject dirC = objMana.MakeObj("PlayerBullet1");
                dirC.transform.position = transform.position;

                Rigidbody2D rigid = dirC.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                break;
            case 2:
                GameObject dirRR = objMana.MakeObj("PlayerBullet1");
                GameObject dirLL = objMana.MakeObj("PlayerBullet1");
                dirRR.transform.position = transform.position + Vector3.right * 0.25f; 
                dirLL.transform.position = transform.position + Vector3.left * 0.25f;

                Rigidbody2D rigidRR = dirRR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidLL = dirLL.GetComponent<Rigidbody2D>();
                rigidRR.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                rigidLL.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                break;
            case 3:
                GameObject dirCCC = objMana.MakeObj("PlayerBullet1");
                dirCCC.transform.position = transform.position;

                Rigidbody2D rigidCCC = dirCCC.GetComponent<Rigidbody2D>();
                rigidCCC.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                break;
            case 4:
                GameObject dirRRRR = objMana.MakeObj("PlayerBullet1");
                GameObject dirCCCC = objMana.MakeObj("PlayerBullet2");
                GameObject dirLLLL = objMana.MakeObj("PlayerBullet1");
                dirRRRR.transform.position = transform.position + Vector3.right * 0.25f;
                dirCCCC.transform.position = transform.position;
                dirLLLL.transform.position = transform.position + Vector3.left * 0.25f;

                Rigidbody2D rigidRRRR = dirRRRR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidCCCC = dirCCCC.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidLLLL = dirLLLL.GetComponent<Rigidbody2D>();
                rigidRRRR.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                rigidCCCC.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                rigidLLLL.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                break;
        }

        curDelay = 0;
    }
    void Skill1()
    {
        if(curSkill1Delay < skill1Delay)
        {
            curSkill1Delay += Time.deltaTime;
            return;
        }
        if (!Input.GetKeyDown(KeyCode.E))
        {
            return;
        }

        hp += 10;
        curSkill1Delay = 0;
    }
    void Skill2()
    {
        if (curSkill2Delay < skill2Delay)
        {
            curSkill1Delay += Time.deltaTime;
            return;
        }
        if (!Input.GetKeyDown(KeyCode.R))
        {
            return;
        }

        boom.gameObject.SetActive(true);
        curSkill2Delay = 0;
    }

    void oilDown()
    {
        if(curOilDownDelay < maxOilDownDelay)
        {
            curOilDownDelay += Time.deltaTime;
            return;
        }
        oil -= 5;
        curOilDownDelay = 0;
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
        spri.color = new Color(1, 1, 1, 0.4f);
        isHit = true;
        gameObject.layer = 10;
    }

    void ReturnColor()
    {
        spri.color = new Color(1, 1, 1, 1);
        isHit = false;
        gameObject.layer = 9;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isHit)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Enemy enem = collision.gameObject.GetComponent<Enemy>();
                OnHit(enem.dmg / 2);
                //적도 같이 데미지 9999 주면서 삭제 시키기
                enem.OnHit(9999);
            }
            if (collision.gameObject.CompareTag("EnemyBullet"))
            {
                Bullet bu = collision.gameObject.GetComponent<Bullet>();
                OnHit(bu.dmg);
                bu.CancelInvoke();
                collision.gameObject.SetActive(false);
            }
        }

        if (collision.gameObject.CompareTag("Item"))
        {
            Item item = collision.gameObject.GetComponent<Item>();
            switch (item.name)
            {
                case "Coin":
                    gameMana.score += 500;
                    break;
                case "HP":
                    if(hp > 30)
                    {
                        hp = 30;
                        gameMana.score += 300;
                    }
                    else
                    {
                        hp += 5;
                    }
                    break;
                case "Power":
                    if(power > 4)
                    {
                        power = 4;
                        gameMana.score = 300;
                    }
                    else
                    {
                        power++;
                    }
                    break;
            }
            collision.gameObject.SetActive(false);
        }
    }
}
