using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Boss : MonoBehaviour
{
    [Header("Boss Stat")]
    [SerializeField] public float hp;
    [SerializeField] float dmg;
    [SerializeField] bool isMove;

    [SerializeField] int curSkillIndex;
    [SerializeField] int curSkillStack;
    [SerializeField] int maxSkillStack;

    [Header("ETC")]
    [SerializeField] ObjectManager objMana;
    [SerializeField] Transform playerT;
    [SerializeField] SpriteRenderer spri;
    [SerializeField] SpawnManager spawnMana;
    [SerializeField] GameManager gameMana;

    [Header("Item")]
    [SerializeField] string[] name;
    [SerializeField] float isItemDrop;

    private void Awake()
    {
        name = new string[] { "None", "None", "None", "None", "None", "Power", "None", "None", "Fuer", "None", "None", "None", "None", "None", "HP", "Coin", "None", "None", "None", "None", "None", "Fuer", "None", "None", "Coin", "None", "None", "None", "None", "Fuer", "Fuer", "None", "Fuer", "None", "Fuer", "None", "HP", "None" };
    }

    private void OnEnable()
    {
        hp = 1000;
        dmg = 10;
        spawnMana.isSpawn = false;
        isMove = true;
        transform.position = new Vector3(0, 10, 0);
    }

    private void Update()
    {
        if (isMove)
        {
            transform.position += Vector3.down * 3 * Time.deltaTime;
            if(transform.position.y <= 3)
            {
                isMove = false;
                Invoke("Think", 1);
            }
        }
    }


    void Think()
    {
        curSkillIndex = Random.Range(0, 3);
        maxSkillStack = Random.Range(3, 5);
        curSkillStack = 0;

        switch(curSkillIndex)
        {
            case 0:
                Invoke("ShotGun", 1);
                break;
            case 1:
                Invoke("PlayerShot", 1);
                break;
            case 2:
                Invoke("ArkShot", 1);
            break;
            case 3:
                Invoke("SixShot", 1);
                break;
            case 5:
                Invoke("EnemSpawn", 1);
                break;
        }
    }

    void ShotGun()
    {
        GameObject dirLL = objMana.MakeObj("BossBullet2");
        GameObject dirL = objMana.MakeObj("BossBullet1");
        GameObject dirR = objMana.MakeObj("BossBullet1");
        GameObject dirRR = objMana.MakeObj("BossBullet2");

        dirLL.transform.position = transform.position + Vector3.left * 0.4f;
        dirL.transform.position = transform.position + Vector3.left * 0.2f;
        dirR.transform.position = transform.position + Vector3.right * 0.2f;
        dirRR.transform.position = transform.position + Vector3.right * 0.4f;

        Rigidbody2D rigidLL = dirLL.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidL = dirL.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidR = dirR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidRR = dirRR.GetComponent<Rigidbody2D>();

        rigidLL.AddForce(Vector3.down * 7, ForceMode2D.Impulse);
        rigidL.AddForce(Vector3.down * 7, ForceMode2D.Impulse);
        rigidR.AddForce(Vector3.down * 7, ForceMode2D.Impulse);
        rigidRR.AddForce(Vector3.down * 7, ForceMode2D.Impulse);

        curSkillStack++;
        if(curSkillStack < maxSkillStack)
        {
            Invoke("ShotGun", 1);
        }
        else
        {
            Invoke("Think", 1);
        }
    }

    void PlayerShot()
    {
        Vector3 dir = playerT.position - transform.position;

        GameObject dirLL = objMana.MakeObj("BossBullet2");
        GameObject dirL = objMana.MakeObj("BossBullet1");
        GameObject dirR = objMana.MakeObj("BossBullet1");
        GameObject dirRR = objMana.MakeObj("BossBullet2");

        dirLL.transform.position = transform.position + Vector3.left * 0.4f;
        dirL.transform.position = transform.position + Vector3.left * 0.2f;
        dirR.transform.position = transform.position + Vector3.right * 0.2f;
        dirRR.transform.position = transform.position + Vector3.right * 0.4f;

        Rigidbody2D rigidLL = dirLL.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidL = dirL.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidR = dirR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidRR = dirRR.GetComponent<Rigidbody2D>();

        rigidLL.AddForce(dir.normalized * 7, ForceMode2D.Impulse);
        rigidL.AddForce(dir.normalized * 7, ForceMode2D.Impulse);
        rigidR.AddForce(dir.normalized * 7, ForceMode2D.Impulse);
        rigidRR.AddForce(dir.normalized * 7, ForceMode2D.Impulse);

        curSkillStack++;
        if (curSkillStack < maxSkillStack)
        {
            Invoke("PlayerShot", 1);
        }
        else
        {
            Invoke("Think", 1);
        }
    }

    void ArkShot()
    {
        Vector3 dirVec = playerT.position - transform.position;
        Vector3 dirvec = new Vector2(Random.Range(-0.2f, 0.2f), Random.Range(0, 0.5f));

        dirVec -= dirvec;

        GameObject dir = objMana.MakeObj("BossBullet1");
        dir.transform.position = transform.position;

        Rigidbody2D rigid = dir.GetComponent<Rigidbody2D>();

        rigid.AddForce(dirVec.normalized * 8, ForceMode2D.Impulse)  ;

        curSkillStack++;
        if (curSkillStack < 30)
        {
            Invoke("ArkShot", 0.3f);
        }
        else
        {
            Invoke("Think", 1);
        }
    }

    void SixShot()
    {
        Vector2[] vecs = new Vector2[] { Vector2.right, Vector2.left, Vector2.up, Vector2.down, (Vector2.down + Vector2.right).normalized, (Vector2.up + Vector2.right).normalized, (Vector2.down + Vector2.left).normalized, (Vector2.up + Vector2.left).normalized };


        foreach (Vector2 s in vecs)
        {
            GameObject dir = objMana.MakeObj("BossBullet1");
            dir.transform.position = transform.position;

            Rigidbody2D rigid = dir.GetComponent<Rigidbody2D>();
            dir.transform.LookAt(s);
            rigid.AddForce(s * 8, ForceMode2D.Impulse);
        }

        curSkillStack++;
        if (curSkillStack < maxSkillStack)
        {
            Invoke("SixShot", 1);
        }
        else
        {
            Invoke("Think", 1);
        }
    }

    void EnemSpawn()
    {
        spawnMana.isSpawn = true;
        Invoke("EnemSpawnFalse", 10);
        Invoke("Think", 1);
    }

    void EnemSpawnFalse()
    {
        spawnMana.isSpawn = false;
    }
    
    
    void OnHit(float lk)
    {
        hp -= lk;
        if (isItemDrop < 50)
        {
            int ranItem = Random.Range(0, name.Length);
            GameObject dir = objMana.MakeObj(name[ranItem]);
            if(dir != null)
            {
                dir.transform.position = transform.position;
            }
            
        }
        else
        {
            isItemDrop += 2;
        }
        

        Effect();

        if (hp <= 0)
        {
            CancelInvoke();
            gameMana.score += 10000;
            gameMana.GameEnd();
            gameObject.SetActive(false);
        }
    }

    void Effect()
    {
        spri.color = new Color(1, 1, 1, 0.6f);
        Invoke("ReturnColor", 0.4f);
    }

    void ReturnColor()
    {
        spri.color = new Color(1, 1, 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Bullet bu = collision.GetComponent<Bullet>();

            OnHit(bu.dmg);
            collision.gameObject.SetActive(false);
        }
    }
}
