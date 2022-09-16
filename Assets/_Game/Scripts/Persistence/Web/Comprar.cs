using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Comprar : MonoBehaviour
{
    public Servidor servidor;
    public GameObject imLoading;
    public dbJugador jugador;


    public void comprarObjeto(int valorCompra)
    {
        StartCoroutine(ComprarObjeto(valorCompra));
    }
    IEnumerator ComprarObjeto(int valorCompra)
    {
        imLoading.SetActive(true);
        string[] datos = new string[4];
        datos[0] = GameController.gameController.jugadorActual.id_jugador.ToString();
        datos[1] = valorCompra.ToString();
        datos[2] = GameController.gameController.jugadorActual.xp.ToString();
        StartCoroutine(servidor.ConsumirServicio("actualiza jugador", datos, PosComprar));
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => !servidor.ocupado);
        imLoading.SetActive(false);
    }

    public void PosComprar()
    {
        switch (servidor.respuesta.codigo)
        {
            case 209: //jugador editado correctamente 
                print(servidor.respuesta.mensaje);
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
}