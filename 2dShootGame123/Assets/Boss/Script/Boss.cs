using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Boss : MonoBehaviour
{
    [Header("BossStats")]
    [SerializeField] float dmg;
    [SerializeField] public float hp;

    [Header("Manager")]
    [SerializeField] ObjManager objMana;
    [SerializeField] GameManager gameMana;

    [Header("BossSkill Stack")]
    [SerializeField] int curSkillStack;
    [SerializeField] int maxSkillStack;
    [SerializeField] int curSkillIndex;

    [Header("ETC")]
    [SerializeField] Transform playerT;
    [SerializeField] bool isMove;
    [SerializeField] SpriteRenderer spri;

    private void OnEnable()
    {
        hp = 1000;
        dmg = 10;
        isMove = true;
        transform.position = new Vector3(0, 6, 0);
    }

    private void Update()
    {
        if (isMove)
        {
            transform.position += Vector3.down * 2 * Time.deltaTime;
            if(transform.position.y <= 3)
            {
                isMove = false;
                Invoke("Think", 1);
            }
        }
    }

    void Think()
    {
        curSkillIndex = Random.Range(0, 4);
        maxSkillStack = Random.Range(5, 8);
        curSkillStack = 0;

        switch(curSkillIndex)
        {
            case 0:
                Invoke("ShotGun", 1);
                break;
            case 1:
                Invoke("PlayerShotGun", 1);
                break;
            case 2:
                Invoke("ArkShot", 0.3f);
                break;
            case 3:
                Invoke("SixShot", 1);
                break;
            case 4:
                Think();
                break;
        }
    }

    void ShotGun()
    {
        GameObject dir1 = objMana.MakeObj("BossBullet2");
        GameObject dir2 = objMana.MakeObj("BossBullet1");
        GameObject dir3 = objMana.MakeObj("BossBullet1");
        GameObject dir4 = objMana.MakeObj("BossBullet2");
        dir1.transform.position = transform.position + Vector3.left * 0.35f;
        dir2.transform.position = transform.position + Vector3.left * 0.15f;
        dir3.transform.position = transform.position + Vector3.right * 0.15f;
        dir4.transform.position = transform.position + Vector3.right * 0.35f;

        Rigidbody2D rigid1 = dir1.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid2 = dir2.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid3 = dir3.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid4 = dir4.GetComponent<Rigidbody2D>();

        rigid1.AddForce(Vector3.down * 8, ForceMode2D.Impulse);
        rigid2.AddForce(Vector3.down * 8, ForceMode2D.Impulse);
        rigid3.AddForce(Vector3.down * 8, ForceMode2D.Impulse);
        rigid4.AddForce(Vector3.down * 8, ForceMode2D.Impulse);

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

    void PlayerShotGun()
    {
        Vector3 dir = playerT.position - transform.position;
        GameObject dir1 = objMana.MakeObj("BossBullet2");
        GameObject dir2 = objMana.MakeObj("BossBullet1");
        GameObject dir3 = objMana.MakeObj("BossBullet1");
        GameObject dir4 = objMana.MakeObj("BossBullet2");
        dir1.transform.position = transform.position + Vector3.left * 0.35f;
        dir2.transform.position = transform.position + Vector3.left * 0.15f;
        dir3.transform.position = transform.position + Vector3.right * 0.15f;
        dir4.transform.position = transform.position + Vector3.right * 0.35f;

        Rigidbody2D rigid1 = dir1.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid2 = dir2.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid3 = dir3.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid4 = dir4.GetComponent<Rigidbody2D>();

        rigid1.AddForce(dir.normalized * 8, ForceMode2D.Impulse);
        rigid2.AddForce(dir.normalized * 8, ForceMode2D.Impulse);
        rigid3.AddForce(dir.normalized * 8, ForceMode2D.Impulse);
        rigid4.AddForce(dir.normalized * 8, ForceMode2D.Impulse);

        curSkillStack++;

        if (curSkillStack < maxSkillStack)
        {
            Invoke("PlayerShotGun", 1);
        }
        else
        {
            Invoke("Think", 1);
        }
    }

    void ArkShot()
    {
        Vector3 vecdir = playerT.position - transform.position;
        Vector3 vecdir2 = new Vector2(Random.Range(-0.2f, 0.2f), Random.Range(0, 5));

        vecdir -= vecdir2;
        GameObject dir = objMana.MakeObj("BossBullet1");
        dir.transform.position = transform.position;

        Rigidbody2D rigid = dir.GetComponent<Rigidbody2D>();
        rigid.AddForce(vecdir.normalized * 7, ForceMode2D.Impulse);

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

    public void OnHit(float dmg)
    {
        hp -= dmg;

        OnHitEffect();
        Invoke("ReturnColor", 0.3f);

        if (hp <= 0)
        {
            gameMana.score += 1000;
            gameObject.SetActive(false);
        }
    }

    void OnHitEffect()
    {
        spri.color = new Color(1, 1, 1, 0.4f);
    }

    void ReturnColor()
    {
        spri.color = new Color(1, 1, 1, 1);
    }

    void SixShot()
    {
        Vector2[] vec = new Vector2[] { Vector2.down, Vector2.up, Vector2.left, Vector2.right, (Vector2.right + Vector2.up).normalized, (Vector2.right + Vector2.down).normalized, (Vector2.left + Vector2.up).normalized, (Vector2.left + Vector2.down).normalized };

        foreach(Vector2 dir in vec)
        {
            GameObject bu = objMana.MakeObj("BossBullet1");
            bu.transform.position = transform.position;
            
            Rigidbody2D rigid = bu.GetComponent<Rigidbody2D>();
            rigid.AddForce(dir * 8, ForceMode2D.Impulse);
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
}
