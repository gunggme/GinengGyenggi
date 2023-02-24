using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] Transform target;
    [SerializeField] float speed;

    private void Update()
    {
        transform.position += Vector3.down * speed * Time.unscaledDeltaTime;

        if(transform.position.y <= -range)
        {
            transform.position = target.position + Vector3.up * range; 
        }
    }
}
