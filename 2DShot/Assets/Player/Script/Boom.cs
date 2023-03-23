using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    private void OnEnable()
    {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("EnemBullet");
        foreach(GameObject dir in bullets)
        {
            dir.SetActive(false);
        }

        GameObject[] enems = GameObject.FindGameObjectsWithTag("Enem");
        foreach(GameObject dir in enems)
        {
            Enemis enem = dir.GetComponent<Enemis>();
            enem.OnHit(99999);
        }
    }
}
