using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Boss : MonoBehaviour
{
    [SerializeField] Transform[] firePosition;

    [Header("Stat")]
    [SerializeField] public float hp;

    [Header("Skill")]
    [SerializeField] int skillIndex;
    [SerializeField] int curSkillStack;
    [SerializeField] int maxSkillStack;

    bool isMove = false;

    [Header("ETC")]
    [SerializeField] SpriteRenderer spri;
    [SerializeField] Transform player;
    [SerializeField] int a = 1;
        
    private void Update()
    {
        if (!isMove)
        {
            transform.position += Vector3.down * 3 * Time.deltaTime;
            if(transform.position.y < 2)
            {
                isMove = true;
                Invoke("Think", 1);
            }
        }
    }

    private void Start()
    {
    }

    void Think()
    {
        skillIndex = Random.Range(0, 3);
        curSkillStack = 0;
        maxSkillStack = Random.Range(3, 6);

        switch(skillIndex)
        {
            case 0:
                Invoke("PlayerShotGun", 1);
                break;
            case 1:
                Invoke("ArkShot", 1);
                break;
            case 2:
                Invoke("EnemSpawn", 1);
                break;
            case 3:
                Think();
                break;
        }
    }

    void PlayerShotGun()
    {
        Vector3 vec = player.position - transform.position;
        Vector3 vec2 = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(0, 0.5f));
        vec -= vec2;

        GameObject dir1 = GameManager.instance.objMana.MakeObj("EnemBullet2");
        GameObject dir2 = GameManager.instance.objMana.MakeObj("EnemBullet2");
        GameObject dir3 = GameManager.instance.objMana.MakeObj("EnemBullet2");
        GameObject dir4 = GameManager.instance.objMana.MakeObj("EnemBullet2");

        dir1.transform.position = firePosition[0].position + Vector3.right * 0.25f;
        dir2.transform.position = firePosition[0].position + Vector3.right * 0.1f;
        dir3.transform.position = firePosition[0].position + Vector3.left * 0.1f;
        dir4.transform.position = firePosition[0].position + Vector3.left * 0.25f;

        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
        dir1.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        dir2.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        dir3.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        dir4.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        dir1.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 8, ForceMode2D.Impulse);
        dir2.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 8, ForceMode2D.Impulse);
        dir3.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 8, ForceMode2D.Impulse);
        dir4.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 8, ForceMode2D.Impulse);

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

    void ArkShot()
    {
        Vector3 vec = player.position - transform.position;
        GameObject dir = GameManager.instance.objMana.MakeObj("EnemBullet1");

        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
        dir.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        a = a == 1 ? 2 : 1;
        dir.transform.position = firePosition[a].position;
        dir.GetComponent<Rigidbody2D>().AddForce(vec.normalized * 10, ForceMode2D.Impulse);
        curSkillStack++;
        if (curSkillStack < 40)
        {
            Invoke("ArkShot", 0.5f);
        }
        else
        {
            Invoke("Think", 1);
        }
    }

    void EnemSpawn()
    {
        CancelInvoke();
        GameManager.instance.spawnMana.isSpawn = true;
        Invoke("EnemSpawnFalse", 15);
        Invoke("Think", 1);
    }

    void EnemSpawnFalse()
    {
        GameManager.instance.spawnMana.isSpawn = true;
    }

    void OnHit(float dmg)
    {
        hp -= dmg;

        OnHitEffect();

        if(hp < 1)
        {
            gameObject.SetActive(false);
        }
    }

    void OnHitEffect()
    {
        spri.color = new Color(1, 1, 1, 0.6f);
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
