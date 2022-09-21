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
    public dbJugador jugador;
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
        datos[0] = usuario.id.ToString();
        datos[1] = "0";
        datos[2] = "0";
        datos[3] = "0";
        StartCoroutine(servidor.ConsumirServicio("crear jugador", datos,PosCrear));
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => !servidor.ocupado);
        imLoading.SetActive(false);
        imLoading.SetActive(false);
        if(validUser){
            StartCoroutine(servidor.ConsumirServicio("buscar jugador", datos, PosBuscar));
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => !servidor.ocupado);
            StartCoroutine(servidor.ConsumirServicio("buscar ingemon", datos,PosBuscarIngemon));
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
                print(servidor.respuesta.mensaje);
                break;
            case 205: //inicio de sesion correcto
                usuario = dbUsuario.CreateFromJSON(servidor.respuesta.respuesta);
                validUser = true;
                break;
            case 404: // Error
                print("Error, no se puede conectar con el servidor");
                break;
            case 402: // faltan datos para ejecutar la accion solicitada
                print(servidor.respuesta.mensaje);
                break;

            default:
                break;
        }
    }
    void PosCrear()
    {
        switch (servidor.respuesta.codigo)
        {
            case 208: //jugador creado correctamente
                print(servidor.respuesta.mensaje);
                break;
            case 404: // Error
                print("Error, no se puede conectar con el servidor");
                break;
            case 402: // faltan datos para ejecutar la accion solicitada
                print(servidor.respuesta.mensaje);
                break;
            case 407: // jugador ya creado
                print(servidor.respuesta.mensaje);             
                break;
            case 408: // error creando el jugador
                print(servidor.respuesta.mensaje);
                break;
            default:
                break;
        }
    }
    void PosBuscar()
    {
        switch (servidor.respuesta.codigo)
        {
            case 209: //jugador encontrado
                print(servidor.respuesta.respuesta);
                jugador = dbJugador.CreateFromJSON(servidor.respuesta.respuesta);
                GameController.gameController.AsignarJugador(jugador);
                // SceneManager.LoadScene(0);
                break;
            case 404: // Error
                print("Error, no se puede conectar con el servidor");
                break;
            case 402: // faltan datos para ejecutar la accion solicitada
                print(servidor.respuesta.mensaje);
                break;
            case 409: // jugador no encontrado
                print(servidor.respuesta.mensaje);
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
                print(servidor.respuesta.mensaje);
                List<string> ingemones = servidor.respuesta.respuesta.Split("!").ToList();
                GameController.gameController.AsignarIngemones(ingemones);
                SceneManager.LoadScene(0);
                break;
            case 404: // Error
                print("Error, no se puede conectar con el servidor");
                break;
            case 402: // faltan datos para ejecutar la accion solicitada
                print(servidor.respuesta.mensaje);
                break;
            case 410: // ingemones no encontrados
                print(servidor.respuesta.mensaje);
                SceneManager.LoadScene(3);
                break;
            default:
                break;
        }
    }
}

