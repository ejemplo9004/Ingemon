using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopSceneController : MonoBehaviour
{
    public Servidor servidor;
    [SerializeField] private IngemonsterGenerator ingemonGenerator;
    [SerializeField] private CardGenerator cardGenerator;
    public ShopUI shopUI;

    public void ComeBackToMenu()
    {
        if (!ingemonGenerator.IsInCreation && !cardGenerator.IsInCreation && GameController.gameController.Inventory.Ingemones.Count >= 4)
        {
            StartCoroutine(buscarIngemones());
            SceneChanger.ChangeScene(Scenes.MENU);
        }
        else
        {
            Debug.Log("Termine de crear el item");
        }
    }

    IEnumerator buscarIngemones()
    {
        string[] datos = new string[1];
        datos[0] = GameController.gameController.usuarioActual.id.ToString();
        StartCoroutine(servidor.ConsumirServicio("buscar ingemon", datos, PosBuscarIngemon));
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => !servidor.ocupado);
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
                    MorionSceneManager.LoadScene((int)Scenes.SHOP);
                }
                else
                {
                    MorionSceneManager.LoadScene((int)Scenes.MENU);
                }
                break;
            case 404: // Error
                Logger.Instance.LogInfo("Error, no se puede conectar con el servidor");
                MorionSceneManager.LoadScene(0);
                break;
            case 402: // faltan datos para ejecutar la accion solicitada
                Logger.Instance.LogInfo(servidor.respuesta.mensaje);
                break;
            case 410: // ingemones no encontrados
                Logger.Instance.LogInfo(servidor.respuesta.mensaje);
                MorionSceneManager.LoadScene((int)Scenes.SHOP);
                break;
            default:
                MorionSceneManager.LoadScene(0);
                break;
        }
    }
}
