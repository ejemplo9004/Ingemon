using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BORRAR : MonoBehaviour
{
    public Transform referencia;

    // Update is called once per frame
    void Update()
    {
        if (referencia == null)
        {
            return;
        }
        transform.position = referencia.position;
        transform.rotation = referencia.rotation;
    }
}
