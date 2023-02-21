using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] public float hp;
    [SerializeField] float speed;
    [SerializeField] float dmg;
    [SerializeField] float score;

    [SerializeField] bool isMove;

    [SerializeField] int curSkillIndex;
    [SerializeField] int totalSkillNum;
    [SerializeField] int maxSkillNum;

    SpriteRenderer spri;
    Transform playerT;
    GameManager gameMana;
    ObjectManager objMana;
    SpawnManager spawnMana;

    private void Awake()
    {
        spri = GetComponent<SpriteRenderer>();
        playerT = GameObject.Find("Player").transform;
        spawnMana = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        gameMana = GameObject.Find("GameManager").GetComponent<GameManager>();
        objMana = GameObject.Find("ObjectManager").GetComponent<ObjectManager>();
    }

    private void OnEnable()
    {
        isMove = true;
        spawnMana.bossOn = true;
        hp = 1000;
        speed = 2;
        score = 10000;
        dmg = 5;
    }

    private void Update()
    {
        if(isMove)
        {
            transform.position += Vector3.down * speed * Time.deltaTime;

            if(transform.position.y <= 3)
            {
                isMove = false;
                Invoke("Think", 1f);
            }
        }
    }

    void Think()
    {
        curSkillIndex = Random.Range(0, 4);
        totalSkillNum = 0;
        maxSkillNum = Random.Range(3, 6);

        switch (curSkillIndex)
        {
            case 0:
                Invoke("ShotGun", 1f);
                break;
            case 1:
                Invoke("PlayerShot", 1f);
                break;
            case 2:
                Invoke("SixShot", 1f);
                break;
            case 3:
                Invoke("ArkShot", 1f);
                break;
            case 4:
                Think();
                break;
        }
    }

    void ShotGun()
    {
        //밑을 향해 4발 발사
        GameObject dirC1 = objMana.MakeObj("BossBullet1");
        GameObject dirC2 = objMana.MakeObj("BossBullet1");
        GameObject dirL = objMana.MakeObj("BossBullet2");
        GameObject dirR = objMana.MakeObj("BossBullet2");

        dirC1.transform.position = transform.position + Vector3.right * 0.15f;
        dirC2.transform.position = transform.position + Vector3.left * 0.15f;
        dirR.transform.position = transform.position + Vector3.right * 0.35f;
        dirL.transform.position = transform.position + Vector3.left * 0.35f;

        Rigidbody2D rigidC1 = dirC1.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidC2 = dirC2.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidR = dirR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidL = dirL.GetComponent<Rigidbody2D>();

        rigidC1.AddForce(Vector2.down * 5, ForceMode2D.Impulse);
        rigidC2.AddForce(Vector2.down * 5, ForceMode2D.Impulse);
        rigidR.AddForce(Vector2.down * 5, ForceMode2D.Impulse);
        rigidL.AddForce(Vector2.down * 5, ForceMode2D.Impulse);

        totalSkillNum++;
        if(totalSkillNum < maxSkillNum)
        {
            Invoke("ShotGun", 0.5f);
        }
        else
        {
            Invoke("Think", 1f);
        }
    }

    void PlayerShot()
    {
        //플레이어를 향해 4발 발사
        GameObject dirC1 = objMana.MakeObj("BossBullet1");
        GameObject dirC2 = objMana.MakeObj("BossBullet1");
        GameObject dirL = objMana.MakeObj("BossBullet2");
        GameObject dirR = objMana.MakeObj("BossBullet2");

        dirC1.transform.position = transform.position + Vector3.right * 0.15f;
        dirC2.transform.position = transform.position + Vector3.left * 0.15f;
        dirR.transform.position = transform.position + Vector3.right * 0.35f;
        dirL.transform.position = transform.position + Vector3.left * 0.35f;

        Vector3 dir = playerT.position - transform.position;

        Rigidbody2D rigidC1 = dirC1.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidC2 = dirC2.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidR = dirR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidL = dirL.GetComponent<Rigidbody2D>();

        rigidC1.AddForce(dir.normalized * 5, ForceMode2D.Impulse);
        rigidC2.AddForce(dir.normalized * 5, ForceMode2D.Impulse);
        rigidR.AddForce(dir.normalized * 5, ForceMode2D.Impulse);
        rigidL.AddForce(dir.normalized * 5, ForceMode2D.Impulse);

        totalSkillNum++;
        if (totalSkillNum < maxSkillNum)
        {
            Invoke("PlayerShot", 0.5f);
        }
        else
        {
            Invoke("Think", 1f);
        }
    }

    void SixShot()
    {
        Vector2[] direction = { Vector2.down, Vector2.up, Vector2.right, Vector2.left, (Vector2.down + Vector2.left).normalized, (Vector2.right + Vector2.down).normalized, (Vector2.left + Vector2.up).normalized, (Vector2.up + Vector2.right).normalized };

        foreach(Vector2 dir in direction)
        {
            GameObject bullet = objMana.MakeObj("BossBullet2");
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;

            bullet.GetComponent<Rigidbody2D>().velocity = dir * 8;
        }

        totalSkillNum++;
        if (totalSkillNum < maxSkillNum)
        {
            Invoke("SixShot", 0.3f);
        }
        else
        {
            Invoke("Think", 1f);
        }
    }

    void ArkShot()
    {
        GameObject dir = objMana.MakeObj("BossBullet1");

        dir.transform.position = transform.position;

        Rigidbody2D rigid = dir.GetComponent<Rigidbody2D>();

        Vector2 dirVec = playerT.position - transform.position;
        Vector2 ranVec = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(0, 2));

        dirVec += ranVec;

        rigid.AddForce(dirVec.normalized * 8, ForceMode2D.Impulse);

        totalSkillNum++;
        if (totalSkillNum < 30)
        {
            Invoke("ArkShot", 0.2f);
        }
        else
        {
            Invoke("Think", 1f);
        }
    }

    void OnHit(float dmg)
    {
        hp -= dmg;

        OnHitEffect();

        if(hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    void OnHitEffect()
    {
        spri.color = new Color(1, 1, 1, 0.4f);
        Invoke("ReturnColor", 0.5f);
    }

    void ReturnColor()
    {
        spri.color = new Color(1, 1, 1, 1);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Bullet playerB = collision.gameObject.GetComponent<Bullet>();
            OnHit(playerB.dmg);
        }

    }
}
