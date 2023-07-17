using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public Servidor     servidor;
    public InputField   inpUsuario;
    public InputField   inpPass;
    public GameObject   imLoading;
    public dbUsuario    usuario;
    private bool        validUser;
    private bool        hasCard;
    public Toggle       tMantener;

    private void Start()
    {
        string us = PlayerPrefs.GetString("usuario", "");
        string ps = PlayerPrefs.GetString("pass", "");
        if (!us.Equals(""))
        {
            inpUsuario.text = us;
        }
        if (!ps.Equals(""))
        {
            inpPass.text = ps;
            iniciarSesion();
        }
    }

    public void iniciarSesion()
    {
        if (inpUsuario.text =="" || inpPass.text == "")
        {
            if (Mensajes.singleton != null)
            {
                Mensajes.singleton.Popup("Todos los datos son Obligatorios");
            }
        }
        else
        {
            StartCoroutine(Iniciar());
        }
        
    }
    IEnumerator Iniciar()
    {
   
        imLoading.SetActive(true);
        string[] datos = new string[4];
        datos[0] = inpUsuario.text;
        datos[1] = inpPass.text.Cifrar();
        StartCoroutine(servidor.ConsumirServicio("login", datos, PosCargar));
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => !servidor.ocupado);
        if(validUser){
            datos[0] = GameController.gameController.usuarioActual.id.ToString();
            StartCoroutine(servidor.ConsumirServicio("buscar cartas usuario", datos, PostSearchPlayerCards));
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => !servidor.ocupado);
            StartCoroutine(servidor.ConsumirServicio("buscar ingemon", datos, PosBuscarIngemon));
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => !servidor.ocupado);
            validUser = false;
        }     
        imLoading.SetActive(false);
        imLoading.SetActive(false);
    } 
    void PosCargar()
    {
        switch (servidor.respuesta.codigo)
        {
            case 204: //usuario o contraseña incorrectos
                Logger.Instance.LogInfo(servidor.respuesta.mensaje);
                if (Mensajes.singleton != null)
                {
                    Mensajes.singleton.Popup("Usuario o contraseña incorrecto");
                }
                break;
            case 205: //inicio de sesion correcto
                usuario = dbUsuario.CreateFromJSON(servidor.respuesta.respuesta);
                validUser = true;
                GameController.gameController.AsignarJugador(usuario);
                Debug.Log("usuario melo");
                if (tMantener.isOn)
                {
                    PlayerPrefs.SetString("usuario", inpUsuario.text);
                    PlayerPrefs.SetString("pass", inpPass.text);
                    Debug.Log("locoo2");
                }
                break;
            case 404: // Error
                Mensajes.singleton.Popup("Error de conexión");
                Debug.Log("locoo5");
                break;
            case 402: // faltan datos para ejecutar la accion solicitada
                Logger.Instance.LogInfo(servidor.respuesta.mensaje);
                break;

            default:
                break;
        }
    }
    void PosBuscarIngemon()
    {
        switch (servidor.respuesta.codigo)
        {
            case 210: //ingemon encontrado
                Logger.Instance.LogInfo(servidor.respuesta.respuesta);
                PlayerPrefs.SetInt("FirstTime", hasCard ? 0 : 1);
                List<string> ingemones = servidor.respuesta.respuesta.Split("!").ToList();
                ingemones.Remove("");
                GameController.gameController.AsignarIngemones(ingemones);
                if (ingemones.Count < 4 || !hasCard)
                {
                    MorionSceneManager.LoadScene((int)Scenes.SHOP);
                }
                else
                {
                    MorionSceneManager.LoadScene((int)Scenes.MENU);
                }
                break;
            case 404: // Error
                Mensajes.singleton.Popup("Error, no se puede conectar con el servidor");
                MorionSceneManager.LoadScene(0);
                Debug.Log(servidor.respuesta.mensaje);
                break;
            case 402: // faltan datos para ejecutar la accion solicitada
                Debug.Log("locoo");
                break;
            case 410: // ingemones no encontrados
                //GameController.gameController.firstTime = true;
                Debug.Log("locoo4");
                PlayerPrefs.SetInt("FirstTime", 1);
                MorionSceneManager.LoadScene((int)Scenes.SHOP);
                break;
            default:
                MorionSceneManager.LoadScene(0);
                break;
        }
    }

    private void PostSearchPlayerCards()
    {
        switch (servidor.respuesta.codigo)
        {
            case 214: //user cards found
                hasCard = servidor.respuesta.respuesta.Length > 0;
                break;
            case 410:
                hasCard = false;
                break;
        }
    }
}

