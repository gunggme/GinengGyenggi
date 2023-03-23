using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    [Header("Boss Stat")]
    public float hp;

    [Header("GameManager")]
    [SerializeField] GameManager gameMana;

    [Header("SkillIndex")]
    [SerializeField] int skillIndex;
    [SerializeField] int curSkillStack;
    [SerializeField] int maxSkillStack;

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
            if(transform.position.y < 2)
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
        hp = 1000;
    }

    void Think()
    {
        skillIndex = Random.Range(0, 3);
        curSkillStack = 0;
        maxSkillStack = Random.Range(5, 10);

        switch(skillIndex)
        {
            case 0:
                Invoke("ShotGun", 1);
                break;
            case 1:
                Invoke("PlayerGun", 1);
                break;
            case 2:
                SpawnOn();
                break;
            case 3:
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
        dir1.transform.position = firePosition[0].transform.position + Vector3.right * 0.1f;
        dir2.transform.position = firePosition[0].transform.position + Vector3.right * 0.05f;
        dir3.transform.position = firePosition[0].transform.position + Vector3.left * 0.05f;
        dir4.transform.position = firePosition[0].transform.position + Vector3.left * 0.1f;
        
        dir1.transform.rotation = Quaternion.identity;
        dir2.transform.rotation = Quaternion.identity;
        dir3.transform.rotation = Quaternion.identity;
        dir4.transform.rotation = Quaternion.identity;

        Rigidbody2D rigid1 = dir1.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid2 = dir2.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid3 = dir3.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid4 = dir4.GetComponent<Rigidbody2D>();

        rigid1.AddForce(Vector3.down * 10, ForceMode2D.Impulse);
        rigid2.AddForce(Vector3.down * 10, ForceMode2D.Impulse);
        rigid3.AddForce(Vector3.down * 10, ForceMode2D.Impulse);
        rigid4.AddForce(Vector3.down * 10, ForceMode2D.Impulse);

        curSkillStack++;
        if(curSkillStack < maxSkillStack)
        {
            Invoke("ShotGun", 0.6f);
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
        dir1.transform.position = firePosition[0].transform.position + Vector3.right * 0.1f;
        dir2.transform.position = firePosition[0].transform.position + Vector3.right * 0.05f;
        dir3.transform.position = firePosition[0].transform.position + Vector3.left * 0.05f;
        dir4.transform.position = firePosition[0].transform.position + Vector3.left * 0.1f;

        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;

        dir1.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        dir2.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        dir3.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        dir4.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        Rigidbody2D rigid1 = dir1.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid2 = dir2.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid3 = dir3.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid4 = dir4.GetComponent<Rigidbody2D>();

        rigid1.AddForce(vec.normalized * 10, ForceMode2D.Impulse);
        rigid2.AddForce(vec.normalized * 10, ForceMode2D.Impulse);
        rigid3.AddForce(vec.normalized * 10, ForceMode2D.Impulse);
        rigid4.AddForce(vec.normalized * 10, ForceMode2D.Impulse);

        curSkillStack++;
        if (curSkillStack < maxSkillStack)
        {
            Invoke("PlayerGun", 0.6f);
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

        if(hp < 1)
        {
            CancelInvoke();
            //GameManager.instance.spawnMana.isSpawn = true;
            GameManager.instance.score += 10000;
            
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
        gameMana.Stage1Over();
    }
}
