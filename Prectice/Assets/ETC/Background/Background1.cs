using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class Background1 : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float speed;
    [SerializeField] float range;
    
    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
        if(transform.position.y < -range)
        {
            transform.position = target.position + Vector3.up * range;
        }
    }
}
