using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public Servidor servidor;
    public InputField inpUsuario;
    public InputField inpPass;
    public GameObject imLoading;
    public void iniciarSesion()
    {
        StartCoroutine(Iniciar());
    } 
    IEnumerator Iniciar()
    {
        imLoading.SetActive(true);
        string[] datos = new string[2];
        datos[0] = inpUsuario.text;
        datos[1] = inpPass.text;
        StartCoroutine(servidor.ConsumirServicio("login", datos));
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => !servidor.ocupado);
        imLoading.SetActive(false);
    }
}
