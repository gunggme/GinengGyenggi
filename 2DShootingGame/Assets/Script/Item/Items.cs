using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public string name;

    private void Update()
    {
        transform.position += Vector3.down * 3 * Time.deltaTime;
    }
}
