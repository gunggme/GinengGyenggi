using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class Boom : MonoBehaviour
{
    private void OnEnable()
    {
        Enemis[] enemis = GameObject.FindWithTag("Enem").GetComponents<Enemis>();
        foreach(Enemis enem in enemis)
        {
            enem.OnHit(9999);
        }

        GameObject[] bullets = GameObject.FindGameObjectsWithTag("EnemBullet");
        foreach(GameObject bullet in bullets)
        {
            bullet.gameObject.SetActive(false);
        }
    }
}
