using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiaFondos : MonoBehaviour
{
    public Sprite[] imagenes;
    public UnityEngine.UI.Image imagen;

    void Start()
    {
        imagen.sprite = imagenes[Random.Range(0, imagenes.Length)];
    }

}
