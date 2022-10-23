using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Registro : MonoBehaviour
{
    public Servidor     servidor;
    public Text         txtMensaje;
    public InputField   inpUsuarioRegistro;
    public InputField   inpPassRegistro;
    public InputField   inpPassRegistro2;
    public InputField   inpJugador;
    public InputField   inpNivel;
    public GameObject   imLoading;

    [Header("Login")]
    public InputField   inpLoginJugador;
    public GameObject   goLogin;
    public GameObject   goRegistrar;

    public void registrarUsuario()
    {
        if (inpUsuarioRegistro.text == "" || inpPassRegistro.text == "" || inpPassRegistro2.text == "")
        {
            //txtMensaje.text = "Todos los datos son Obligatorios";
            if (Mensajes.singleton != null)
            {
                Mensajes.singleton.Popup("Todos los datos son Obligatorios");
            }
        }
        if (inpPassRegistro.text == inpPassRegistro2.text)
        {
            StartCoroutine(Registrar());
        }
        else
        {
            //txtMensaje.text = "Las contrase�as no coinciden";
            if (Mensajes.singleton != null)
            {
                Mensajes.singleton.Popup("Las contraseñas no coinciden");
            }
        }
    }
    IEnumerator Registrar()
    {
        imLoading.SetActive(true);
        string[] datos = new string[3];
        datos[0] = inpUsuarioRegistro.text;
        datos[1] = inpPassRegistro.text.Cifrar();
        datos[2] = "150";
        StartCoroutine(servidor.ConsumirServicio("registrar usuario", datos, posRegistro));
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => !servidor.ocupado);
        imLoading.SetActive(false);
        if(servidor.respuesta.codigo == 201)
        {
            Mensajes.singleton.Popup("Usuario Registrado Exitosamente");
            inpLoginJugador.text = inpUsuarioRegistro.text;
            goRegistrar.SetActive(false);
            goLogin.SetActive(true);
        }
    }

    void posRegistro()
    {
        
    }
}

