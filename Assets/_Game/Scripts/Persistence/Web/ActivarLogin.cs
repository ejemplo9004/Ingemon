using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarLogin : MonoBehaviour
{
    public GameObject imlogin;
    public GameObject imRegistro;
    public GameObject imEditar;
    public void activar()
    {
        if (imlogin.activeSelf == false && imEditar.activeSelf == true)
        {
            imlogin.SetActive(true);
            imEditar.SetActive(false);

        } else if (imlogin.activeSelf == true)
        {
            imlogin.SetActive(false);
            imRegistro.SetActive(true);
        } else if (imlogin.activeSelf == false) 
        {
            imlogin.SetActive(true);
            imRegistro.SetActive(false);
            
        }
    }
}


