using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BorrarCuenta : MonoBehaviour
{
    public Servidor servidor;
    public InputField passwordField;
    private bool usuarioBorrado;

    public void BorrarUsuario(){
        StartCoroutine(Borrar());
    }

    IEnumerator Borrar(){
        string[] datos = new string[2];
        datos[0] = GameController.gameController.usuarioActual.name;
        datos[1] = passwordField.text.Cifrar();
        StartCoroutine(servidor.ConsumirServicio("borrar usuario", datos, PosBorraCuenta));
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => !servidor.ocupado);
        if(usuarioBorrado){
            datos[0] = GameController.gameController.usuarioActual.id.ToString();
            StartCoroutine(servidor.ConsumirServicio("borrar ingemones", datos, PosBorrarIngemones));
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => !servidor.ocupado);
            usuarioBorrado = false;
        }
    }

    private void PosBorraCuenta(){
        switch(servidor.respuesta.codigo){
            case 215:
                Mensajes.singleton.Popup("Usuario borrado correctamente.");
                usuarioBorrado = true;
                break;
            case 204: //usuario o contrase�a incorrectos
                Logger.Instance.LogInfo(servidor.respuesta.mensaje);
                if (Mensajes.singleton != null)
                {
                    Mensajes.singleton.Popup("Usuario o contraseña incorrecto");
                }
                break;
            case 404: // Error
                Mensajes.singleton.Popup("Error en el servidor");
                break;
            default:
                break;
        }
    }

    private void PosBorrarIngemones(){
        switch(servidor.respuesta.codigo){
            case 216:
                Debug.Log("entro al ingem");
                Debug.Log("Se borraron los ingemones.");
                MorionSceneManager.LoadScene((int)Scenes.LOGIN);
                break;
            case 404: // Error
                Debug.Log("ingemones error");
                //Mensajes.singleton.Popup("Error en el servidor");
                break;
            default:
                break;
        }
    }
}
