using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("FalseObject", 0.5f);
    }

    void FalseObject()
    {
        gameObject.SetActive(false);
    }

    
}
