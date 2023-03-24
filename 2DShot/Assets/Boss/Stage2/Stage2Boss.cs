using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2Boss : MonoBehaviour
{
    [Header("stat")]
    public float hp;

    [Header("BossSkill")]
    [SerializeField] int skillIndex;
    [SerializeField] int curSkillStack;
    [SerializeField] int maxSkillStack;

    [Header("ETC")]
    [SerializeField]
    Transform[] firePosition;
    [SerializeField] SpriteRenderer spri;
    [SerializeField] Transform playerT;

    bool isMove = true;

    private void Start()
    {
        hp = 2500;
    }

    private void OnEnable()
    {
        GameManager.instance.spawnMana.isSpawn = false;
    }

    private void Update()
    {
        if (isMove)
        {
            transform.position += Vector3.down * 4 * Time.deltaTime;
            if (transform.position.y < 2)
            {
                isMove = false;
                Invoke("Think", 1);
            }
        }
    }

    void Think()
    {
        skillIndex = Random.Range(0, 4);
        curSkillStack = 0;
        maxSkillStack = Random.Range(5, 8);

        switch (skillIndex)
        {
            case 0:
                Invoke("RoundAttack", 1);
                break;
            case 1:
                Invoke("ArkShot", 1);
                break;
            case 2:
                Invoke("PlayerShot", 1);
                break;
            case 3:
                Invoke("EnemSpawn", 1);
                break;
            case 4:
                Invoke("EnemSpawn", 1);
                break;
        }
    }


    void PlayerShot()
    {
        GameObject dir1 = GameManager.instance.objMana.MakeObj("EnemBullet3");
        GameObject dir2 = GameManager.instance.objMana.MakeObj("EnemBullet3");
        GameObject dir3 = GameManager.instance.objMana.MakeObj("EnemBullet3");
        GameObject dir4 = GameManager.instance.objMana.MakeObj("EnemBullet3");


        //각도를 플레이어 바라보게 만들기
        Vector3 vec = playerT.position - transform.position;
        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
        dir1.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        dir2.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        dir3.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        dir4.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        //발사 위치 지정
        dir1.transform.position = firePosition[0].position + Vector3.right * 0.6f;
        dir2.transform.position = firePosition[0].position + Vector3.right * 0.4f;
        dir3.transform.position = firePosition[0].position + Vector3.left * 0.4f;
        dir4.transform.position = firePosition[0].position + Vector3.left * 0.6f;

        //addForce를 이용해 발사
        dir1.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 13, ForceMode2D.Impulse);
        dir2.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 13, ForceMode2D.Impulse);
        dir3.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 13, ForceMode2D.Impulse);
        dir4.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 13, ForceMode2D.Impulse);

        //여러번 사용하게 만들기
        curSkillStack++;
        if (curSkillStack < maxSkillStack)
        {
            Invoke("PlayerShot", 0.8f);
        }
        else
        {
            Invoke("Think", 1);
        }
    }

    void RoundAttack()
    {
        int round = 20;
        for (int i = 0; i < round; i++)
        {
            Vector2 vec = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / round), Mathf.Sin(Mathf.PI * 2 * i / round));
            GameObject dir = GameManager.instance.objMana.MakeObj("EnemBullet3");
            GameObject dir2 = GameManager.instance.objMana.MakeObj("EnemBullet3");
            dir.transform.position = firePosition[1].position;
            dir2.transform.position = firePosition[2].position;

            dir.transform.rotation = Quaternion.identity;
            dir2.transform.rotation = Quaternion.identity;

            dir.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 2, ForceMode2D.Impulse);
            dir2.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 2, ForceMode2D.Impulse);
        }

        //여러번 사용하게 만들기
        curSkillStack++;
        if (curSkillStack < 3)
        {
            Invoke("RoundAttack", 0.4f);
        }
        else
        {
            Invoke("Think", 1);
        }
    }

    void ArkShot()
    {
        Vector3 vec = playerT.position - transform.position;
        GameObject dir1 = GameManager.instance.objMana.MakeObj("EnemBullet2");
        dir1.transform.position = firePosition[0].position;

        float sngle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
        dir1.transform.rotation = Quaternion.AngleAxis(sngle - 90 , Vector3.forward);

        if (curSkillStack < 20)
        {
            Invoke("RoundAttack", 0.2f);
        }
        else
        {
            Invoke("Think", 1);
        }
    }

    void EnemSpawn()
    {
        GameManager.instance.spawnMana.isSpawn = true;
        Think();
        Invoke("EnemSpawnOff", 10);
    }

    void EnemSpawnOff()
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
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Bullet bu = collision.gameObject.GetComponent<Bullet>();
            OnHit(bu.dmg);
            collision.gameObject.SetActive(false);
        }
    }
}
