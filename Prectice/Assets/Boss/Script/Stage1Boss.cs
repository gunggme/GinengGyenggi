using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Stage1Boss : MonoBehaviour
{
    [Header("Stat")]
    public float hp;

    [Header("Skill")]
    [SerializeField] int skillIndex;
    [SerializeField] int maxSkillStack;
    [SerializeField] int curSkillStack;

    [Header("ETC")]
    [SerializeField]
    Transform[] firePosition;
    [SerializeField] Transform playerT;
    [SerializeField] SpriteRenderer spri;

    [SerializeField] bool isMove;

    private void OnEnable()
    {
        isMove = true;
    }

    private void Update()
    {
        if (isMove)
        {
            transform.position += Vector3.down * 3 * Time.deltaTime;
            if(transform.position.y < 2)
            {
                isMove = false;
                Invoke("Think", 1);
            }
        }
    }

    void Think()    
    {
        curSkillStack = 0;
        maxSkillStack = Random.Range(4, 7);
        skillIndex = Random.Range(0, 4);

        switch(skillIndex)
        {
            case 0:
                Invoke("PlayerShotGun", 1);
                break;
            case 1:
                Invoke("CsicleShot", 1);
                break;
            case 2:
                Invoke("ArkShot", 1);
                break;
            case 3:
                Invoke("SpawnOn", 1);
                break;
            case 4:
                Think();
                break;

        }
    }

    void PlayerShotGun()
    {
        Vector3 vec = playerT.position - transform.position;

        GameObject dir1 = GameManager.instance.objMana.MakeObj("EnemBullet2");
        GameObject dir2 = GameManager.instance.objMana.MakeObj("EnemBullet2");
        GameObject dir3 = GameManager.instance.objMana.MakeObj("EnemBullet2");
        GameObject dir4 = GameManager.instance.objMana.MakeObj("EnemBullet2");

        dir1.transform.position = firePosition[0].transform.position + Vector3.right * 0.4f;
        dir2.transform.position = firePosition[0].transform.position + Vector3.right * 0.2f;
        dir3.transform.position = firePosition[0].transform.position + Vector3.left * 0.2f;
        dir4.transform.position = firePosition[0].transform.position + Vector3.left * 0.4f;

        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
        dir1.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        dir2.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        dir3.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        dir4.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        dir1.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 13, ForceMode2D.Impulse);
        dir2.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 13, ForceMode2D.Impulse);
        dir3.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 13, ForceMode2D.Impulse);
        dir4.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 13, ForceMode2D.Impulse);

        curSkillStack++;
        if(curSkillStack < maxSkillStack)
        {
            Invoke("PlayerShotGun", 0.5f);
        }
        else
        {
            Invoke("Think", 1);
        }
    }

    void CsicleShot()
    {
        int round = 10;
        for(int i = 0; i < round; i++)
        {
            Vector3 vec = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / round), Mathf.Sin(Mathf.PI * 2 * i / round));
            GameObject dir1 = GameManager.instance.objMana.MakeObj("EnemBullet3");
            GameObject dir2 = GameManager.instance.objMana.MakeObj("EnemBullet3");

            dir1.transform.position = firePosition[1].position;
            dir2.transform.position = firePosition[2].position;
            dir1.transform.rotation = Quaternion.identity;
            dir2.transform.rotation = Quaternion.identity;

            dir1.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 7, ForceMode2D.Impulse);
            dir2.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 7, ForceMode2D.Impulse);
        }

        curSkillStack++;
        if (curSkillStack < maxSkillStack)
        {
            Invoke("CsicleShot", 0.8f);
        }
        else
        {
            Invoke("Think", 1);
        }
    }

    void ArkShot()
    {
        Vector3 vec = playerT.position - transform.position;
        GameObject dir = GameManager.instance.objMana.MakeObj("EnemBullet1");
        dir.transform.position = firePosition[0].position;

        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
        dir.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        dir.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 10, ForceMode2D.Impulse);
        curSkillStack++;
        if (curSkillStack < 20)
        {
            Invoke("ArkShot", 0.2f);
        }
        else
        {
            Invoke("Think", 1);
        }
    }
    
    void SpawnOn()
    {
        GameManager.instance.spawnMana.isSpawn = true;
        Invoke("SpawnOff", 15f);
        Think();
    }

    void SpawnOff()
    {
        GameManager.instance.spawnMana.isSpawn = false;
    }

    public void OnHit(float dmg)
    {
        hp -= dmg;

        OnHitEffect();

        if (hp < 1)
        {
            CancelInvoke();
            GameManager.instance.score += 1000;
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
            Bullet bu = collision.GetComponent<Bullet>();
            OnHit(bu.dmg);
            collision.gameObject.SetActive(false);
        }
    }
}
