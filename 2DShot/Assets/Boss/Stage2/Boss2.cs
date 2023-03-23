using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : MonoBehaviour
{
    [Header("Boss Stat")]
    public float hp;

    [Header("SkillIndex")]
    [SerializeField] int skillIndex;
    [SerializeField] int curSkillStack;
    [SerializeField] int maxSkillStack;

    [Header("GameManager")]
    [SerializeField] GameManager gameMana;

    [Header("ETC")]
    [SerializeField] Transform[] firePosition;
    [SerializeField] bool isMove = true;
    [SerializeField] Transform playerT;
    [SerializeField] SpriteRenderer spri;

    private void Update()
    {
        if (isMove)
        {
            transform.position += Vector3.down * 3 * Time.deltaTime;
            if (transform.position.y < 2)
            {
                isMove = false;
                Think();
            }
        }
    }

    private void OnEnable()
    {
        //적 스폰 제어
        GameManager.instance.spawnMana.isSpawn = false;
        hp = 2000;
    }

    void Think()
    {
        skillIndex = Random.Range(0, 6);
        curSkillStack = 0;
        maxSkillStack = Random.Range(5, 10);

        switch (skillIndex)
        {
            case 0:
                Invoke("ShotGun", 1);
                break;
            case 1:
                Invoke("PlayerGun", 1);
                break;
            case 3:
                SpawnOn();
                break;
            case 4:
                Invoke("ArkShot", 1);
                break;
            case 5:
                Invoke("SixShot", 1);
                break;
            case 6:
                Think();
                break;
        }
    }

    void ShotGun()
    {
        GameObject dir1 = GameManager.instance.objMana.MakeObj("EnemBullet1");
        GameObject dir2 = GameManager.instance.objMana.MakeObj("EnemBullet1");
        GameObject dir3 = GameManager.instance.objMana.MakeObj("EnemBullet1");
        GameObject dir4 = GameManager.instance.objMana.MakeObj("EnemBullet1");
        dir1.transform.position = firePosition[1].transform.position + Vector3.right * 0.1f;
        dir2.transform.position = firePosition[1].transform.position + Vector3.right * 0.05f;
        dir3.transform.position = firePosition[2].transform.position + Vector3.left * 0.05f;
        dir4.transform.position = firePosition[2].transform.position + Vector3.left * 0.1f;

        dir1.transform.rotation = Quaternion.identity;
        dir2.transform.rotation = Quaternion.identity;
        dir3.transform.rotation = Quaternion.identity;
        dir4.transform.rotation = Quaternion.identity;

        Rigidbody2D rigid1 = dir1.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid2 = dir2.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid3 = dir3.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid4 = dir4.GetComponent<Rigidbody2D>();

        rigid1.AddForce(Vector3.down * 13, ForceMode2D.Impulse);
        rigid2.AddForce(Vector3.down * 13, ForceMode2D.Impulse);
        rigid3.AddForce(Vector3.down * 13, ForceMode2D.Impulse);
        rigid4.AddForce(Vector3.down * 13, ForceMode2D.Impulse);

        curSkillStack++;
        if (curSkillStack < maxSkillStack)
        {
            Invoke("ShotGun", 0.4f);
        }
        else
        {
            Invoke("Think", 1);
        }
    }

    void PlayerGun()
    {
        Vector3 vec = playerT.position - transform.position;

        GameObject dir1 = GameManager.instance.objMana.MakeObj("EnemBullet1");
        GameObject dir2 = GameManager.instance.objMana.MakeObj("EnemBullet1");
        GameObject dir3 = GameManager.instance.objMana.MakeObj("EnemBullet1");
        GameObject dir4 = GameManager.instance.objMana.MakeObj("EnemBullet1");
        dir1.transform.position = firePosition[1].transform.position + Vector3.right * 0.1f;
        dir2.transform.position = firePosition[1].transform.position + Vector3.right * 0.05f;
        dir3.transform.position = firePosition[2].transform.position + Vector3.left * 0.05f;
        dir4.transform.position = firePosition[2].transform.position + Vector3.left * 0.1f;

        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;

        dir1.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        dir2.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        dir3.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        dir4.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        Rigidbody2D rigid1 = dir1.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid2 = dir2.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid3 = dir3.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid4 = dir4.GetComponent<Rigidbody2D>();

        rigid1.AddForce(vec.normalized * 13, ForceMode2D.Impulse);
        rigid2.AddForce(vec.normalized * 13, ForceMode2D.Impulse);
        rigid3.AddForce(vec.normalized * 13, ForceMode2D.Impulse);
        rigid4.AddForce(vec.normalized * 13, ForceMode2D.Impulse);

        curSkillStack++;
        if (curSkillStack < maxSkillStack)
        {
            Invoke("PlayerGun", 0.4f);
        }
        else
        {
            Invoke("Think", 1);
        }
    }

    void ArkShot()
    {
        Vector2 vec = playerT.position - transform.position;
        Vector2 vec1 = new Vector2(Random.Range(-0.2f, 0.2f), Random.Range(0, 0.5f));
        Vector2 vec2 = vec - vec1;

        GameObject dir1 = GameManager.instance.objMana.MakeObj("EnemBullet2");
        int firePoi = Random.Range(0, 1);
        float angle = Mathf.Atan2(vec2.y, vec2.x) * Mathf.Rad2Deg;

        dir1.transform.position = firePosition[firePoi].transform.position;
        dir1.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);


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
        Vector2[] vec = new Vector2[] { Vector2.down, Vector2.up, Vector2.right, Vector2.left, (Vector2.right + Vector2.up).normalized, (Vector2.right + Vector2.down).normalized, (Vector2.left + Vector2.up).normalized, (Vector2.left + Vector2.down).normalized };

        foreach (Vector2 vec2 in vec)
        {
            GameObject dir = GameManager.instance.objMana.MakeObj("EnemBullet3");
            dir.transform.position = firePosition[0].position;

            Rigidbody2D rigid = dir.GetComponent<Rigidbody2D>();
            rigid.AddForce(vec2 * 13, ForceMode2D.Impulse);
        }

        curSkillStack++;
        if (curSkillStack < maxSkillStack)
        {
            Invoke("SixShot", 0.3f);
        }
        else
        {
            Invoke("Think", 1);
        }
    }

    void SpawnOn()
    {
        GameManager.instance.spawnMana.isSpawn = true;
        Invoke("Think", 1);
        Invoke("SpawnOff", 25f);
    }

    void SpawnOff()
    {
        GameManager.instance.spawnMana.isSpawn = false;
    }

    void OnHit(float dmg)
    {
        hp -= dmg;

        OnHitEffect();

        if (hp < 1)
        {
            CancelInvoke();
            //GameManager.instance.spawnMana.isSpawn = true;
            GameManager.instance.score += 30000;
            gameObject.SetActive(false);
        }
    }

    void OnHitEffect()
    {
        spri.color = new Color(1, 1, 1, 0.5f);
        Invoke("ReturnColor", 0.3f);
    }

    void ReturnColor()
    {
        spri.color = new Color(1, 1, 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemBullet"))
            return;
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Bullet bu = collision.GetComponent<Bullet>();
            OnHit(bu.dmg);
            collision.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        
    }
}
