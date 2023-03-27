using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    private void OnEnable()
    {
        Enemis[] enem = GameObject.FindObjectsOfType<Enemis>();
        foreach(Enemis e in enem)
        {
            e.OnHit(999);
        }
        GameObject[] bu = GameObject.FindGameObjectsWithTag("EnemBullet");
        foreach(GameObject go in bu)
        {
            go.gameObject.SetActive(false);
        }
    }
}
