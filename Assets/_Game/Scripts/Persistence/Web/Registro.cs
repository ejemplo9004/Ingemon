using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Registro : MonoBehaviour
{
    public Servidor servidor;
    public InputField inpUsuarioRegistro;
    public InputField inpPassRegistro;
    public InputField inpJugador;
    public InputField inpNivel;
    public GameObject imLoading;

    public void registrarUsuario()
    {
        StartCoroutine(Registrar());
    }
    IEnumerator Registrar()
    {
        imLoading.SetActive(true);
        string[] datos = new string[4];
        datos[0] = inpUsuarioRegistro.text;
        datos[1] = inpPassRegistro.text;
        datos[2] = inpJugador.text;
        datos[3] = inpNivel.text;
        StartCoroutine(servidor.ConsumirServicio("registrar usuario", datos, null));
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => !servidor.ocupado);
        imLoading.SetActive(false);
    }
}