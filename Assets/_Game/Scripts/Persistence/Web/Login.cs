using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public Servidor servidor;
    public InputField inpUsuario;
    public InputField inpPass;
    public GameObject imLoading;
    public dbUsuario usuario;
    private bool validUser;


    public void iniciarSesion()
    {
        StartCoroutine(Iniciar());
    }
    IEnumerator Iniciar()
    {
        imLoading.SetActive(true);
        string[] datos = new string[4];
        datos[0] = inpUsuario.text;
        datos[1] = inpPass.text;
        StartCoroutine(servidor.ConsumirServicio("login", datos, PosCargar));
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => !servidor.ocupado);
        if(validUser){
            
            datos[0] = GameController.gameController.usuarioActual.id.ToString();
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
            case 204: //usuario o contrase�a incorrectos
                Logger.Instance.LogInfo(servidor.respuesta.mensaje);
                
                break;
            case 205: //inicio de sesion correcto
                usuario = dbUsuario.CreateFromJSON(servidor.respuesta.respuesta);
                validUser = true;
                GameController.gameController.AsignarJugador(usuario);
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
    void PosBuscarIngemon()
    {
        switch (servidor.respuesta.codigo)
        {
            case 210: //ingemon encontrado
                Logger.Instance.LogInfo(servidor.respuesta.respuesta);
                List<string> ingemones = servidor.respuesta.respuesta.Split("!").ToList();
                ingemones.Remove("");
                GameController.gameController.AsignarIngemones(ingemones);
                if (ingemones.Count < 4)
                {
                    SceneManager.LoadScene((int)Scenes.SHOP);
                }
                else
                {
                    SceneManager.LoadScene((int)Scenes.MENU);
                }
                break;
            case 404: // Error
                Logger.Instance.LogInfo("Error, no se puede conectar con el servidor");
                SceneManager.LoadScene(0);
                break;
            case 402: // faltan datos para ejecutar la accion solicitada
                Logger.Instance.LogInfo(servidor.respuesta.mensaje);
                break;
            case 410: // ingemones no encontrados
                Logger.Instance.LogInfo(servidor.respuesta.mensaje);
                SceneManager.LoadScene((int)Scenes.SHOP);
                break;
            default:
                SceneManager.LoadScene(0);
                break;
        }
    }
    
    
}

