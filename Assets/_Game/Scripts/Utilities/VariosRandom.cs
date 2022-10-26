using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariosRandom : MonoBehaviour
{
    public void CerrarSesion()
    {
        PlayerPrefs.SetString("pass", "");
        MorionSceneManager.LoadScene("Login");
        print("Sesion cerrada");
    }

    public void Salir()
    {
        Application.Quit();
    }
}
