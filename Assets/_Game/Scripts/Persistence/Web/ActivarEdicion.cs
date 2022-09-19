using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarEdicion : MonoBehaviour
{
    public GameObject imlogin;
    public GameObject imRegistro;
    public GameObject imEditar;

    public void cambiar()
    {
        if(imlogin.activeSelf == false && imRegistro.activeSelf == false && imEditar.activeSelf == true)
        {
            imEditar.SetActive(false);
            imlogin.SetActive(false);
            imRegistro.SetActive(true);
        } else if (imlogin.activeSelf == true || imRegistro.activeSelf == true && imEditar.activeSelf == false)
        {
            imEditar.SetActive(true);
            imlogin.SetActive(false);
            imRegistro.SetActive(false);
        }
    }
}
