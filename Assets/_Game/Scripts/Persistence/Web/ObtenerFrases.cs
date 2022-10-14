using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class ObtenerFrases : MonoBehaviour
{
    public Servidor servidor;
    public void buscarFrases()
    {
        StartCoroutine(buscar());
    }

    IEnumerator buscar()
    {
        string[] datos = new string[1];
        StartCoroutine(servidor.ConsumirServicio("buscar frases",datos, posBuscar));
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => !servidor.ocupado);
    }
    
    void posBuscar()
    {
        switch (servidor.respuesta.codigo)
        {
            case 211: //frases encontradas correctamente
                List<string> frases = servidor.respuesta.respuesta.Split("!").ToList();
                frases.Remove("");
                var JsonString =  JObject.Parse(frases[0]);
                Debug.Log(JsonString["content"]);
                for (int i = 1; i < frases.Count; i++)
                { 
                    JsonString = JObject.Parse(frases[i]);
                    Debug.Log(JsonString["content"]);
                }
                //Debug.Log(frases[2].Split('"')[5]);
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
