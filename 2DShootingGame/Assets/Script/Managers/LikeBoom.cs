using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LikeBoom : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss"))
        {
            GameObject[] Enemis = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject enem in Enemis)
            {
                if (enem == null)
                    return;
                Enemys e = enem.GetComponent<Enemys>();
                e.OnHit(9999);
            }
            Boss bossS = collision.gameObject.GetComponent<Boss>();
            if(bossS == null)
            {
                return;
            }
            bossS.OnHit(9999);
        }
    }
}
