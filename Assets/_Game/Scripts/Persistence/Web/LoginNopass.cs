using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginNopass : MonoBehaviour
{
    public Servidor servidor;
    public InputField inpUsuario;
    public GameObject imgLoading;
    public dbUsuario usuario;


    public void SesionNoPass()
    {
        StartCoroutine(NoPass());
    }

    IEnumerator NoPass()
    {
        imgLoading.SetActive(true);
        string[] datos = new string[1];
        datos[0] = inpUsuario.text;
        StartCoroutine(servidor.ConsumirServicio("user vr", datos, PosNoPass));
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => !servidor.ocupado);
        imgLoading.SetActive(false);
    }
    
    void PosNoPass()
    {
        switch (servidor.respuesta.codigo)
        {
            case 204: //usuario o contrase�a incorrectos
                Logger.Instance.LogInfo(servidor.respuesta.mensaje);
                break;
            case 205: //inicio de sesion correcto
                usuario = dbUsuario.CreateFromJSON(servidor.respuesta.respuesta);
                GameController.gameController.AsignarJugador(usuario);
                Debug.Log("usuario melo");
                break;
            case 404: // Error
                Logger.Instance.LogWarning("Error, no se puede conectar con el servidor");
                Logger.Instance.LogWarning($"{servidor.respuesta.respuesta}");
                break;
            case 402: // faltan datos para ejecutar la accion solicitada
                Logger.Instance.LogInfo(servidor.respuesta.mensaje);
                break;
            default:
                break;
        }
    }

}
