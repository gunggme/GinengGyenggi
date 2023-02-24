using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("Off", 0.3f);
    }

    void Off()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyBullet") || collision.gameObject.CompareTag("BossBullet"))
        {
            GameObject[] enemis = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject go in enemis)
            {
                Enemys enem = go.GetComponent<Enemys>();
                enem.OnHit(9999);
            }

            GameObject[] bullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
            foreach(GameObject go in bullets)
            {
                go.gameObject.SetActive(false);
            }

            GameObject[] bulletsB = GameObject.FindGameObjectsWithTag("EnemyBullet");
            foreach(GameObject go in bulletsB)
            {
                go.gameObject.SetActive(false);
            }
        }
    }
}
