using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girador : MonoBehaviour
{
    public Vector3 velocidad;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(velocidad * Time.deltaTime);
    }
}
