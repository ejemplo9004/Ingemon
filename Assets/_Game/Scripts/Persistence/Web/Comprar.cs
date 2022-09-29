using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Comprar : MonoBehaviour
{
    public Servidor servidor;
    public GameObject imLoading;
    public dbUsuario usuario;
    public Ingemonster ingemonNuevo;
    private bool bought;


    public void comprarObjeto(int valorCompra)
    {
        StartCoroutine(ComprarObjeto(valorCompra));
    }
    IEnumerator ComprarObjeto(int valorCompra)
    {
        imLoading.SetActive(true);
        string[] datos = new string[4];
        datos[0] = GameController.gameController.usuarioActual.id.ToString();
        datos[1] = valorCompra.ToString();
        StartCoroutine(servidor.ConsumirServicio("actualiza jugador", datos, PosComprar));
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => !servidor.ocupado);
        yield return new WaitUntil(() => bought);
        datos[0] = ingemonNuevo.name;
        datos[1] = ingemonNuevo.phenotype;
        datos[2] = ingemonNuevo.maxHealth.ToString();
        datos[3] = GameController.gameController.usuarioActual.id.ToString();
        StartCoroutine(servidor.ConsumirServicio("guardar ingemon", datos, PosIngemon));
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => !servidor.ocupado);
        bought = false;
        imLoading.SetActive(false);
    }

    public void PosComprar()
    {
        switch (servidor.respuesta.codigo)
        {
            case 209: //jugador editado correctamente 
                print(servidor.respuesta.mensaje);
                bought = true;
                break;
            case 404: // Error
                print("Error, no se puede conectar con el servidor");
                break;
            case 402: // faltan datos para ejecutar la accion solicitada
                print(servidor.respuesta.mensaje);
                break;
            case 210: // jugador no existe
                print(servidor.respuesta.mensaje);
                break;
            case 408: // error creando el jugador
                print(servidor.respuesta.mensaje);
                break;
            default:
                break;
        }

    }
    void PosIngemon()
    {
        switch (servidor.respuesta.codigo)
        {
            case 207: //ingemon guardado correctamente 
                print(servidor.respuesta.mensaje);
                break;
            case 406: //error intentando crear ingemon 
                print(servidor.respuesta.respuesta);
                break;
            case 405: // ya existe un ingemon con este fenotipo
                print(servidor.respuesta.respuesta);
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
}