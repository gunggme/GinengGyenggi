using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    private void OnEnable()
    {
        GameObject[] enems = GameObject.FindGameObjectsWithTag("Enem");
        foreach(GameObject enem in enems)
        {
            Enem enem1 = enem.GetComponent<Enem>();
            enem1.OnHit(9999);
        }
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("EnemBullet");
        foreach(GameObject bullet in bullets)
        {
            bullet.gameObject.SetActive(false);
        }
    }
}
