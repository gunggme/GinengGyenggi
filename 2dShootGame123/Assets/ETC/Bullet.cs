using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float dmg;

    private void OnEnable()
    {
        Invoke("FalseBullet", 5);
    }

    void FalseBullet()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            CancelInvoke();
            gameObject.SetActive(false);
        }
        if(gameObject.CompareTag("EnemyBullet") || gameObject.CompareTag("BossBullet"))
        {
            gameObject.SetActive(false);
        }
    }
}
