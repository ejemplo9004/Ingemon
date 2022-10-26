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

    private void Start()
    {
        CheckIngemonCount();
    }

    public void ComeBackToMenu()
    {
        if (!ingemonGenerator.IsInCreation && !cardGenerator.IsInCreation && GameController.gameController.Inventory.Ingemones.Count >= 4)
        {
            StartCoroutine(buscarIngemones());
            SceneChanger.ChangeScene(Scenes.MENU);
        }
        else if(GameController.gameController.Inventory.Ingemones.Count < 4)
        {
            if (Mensajes.singleton != null)
            {
                Mensajes.singleton.Popup("Debes poseer al menos 4 Ingemones");
            }
        }
        else
        {
            Debug.Log("Termine de crear el item");
        }
    }

    private void CheckIngemonCount()
    {
        if(GameController.gameController.Inventory.Ingemones.Count < 4 && Mensajes.singleton != null)
        {
            int count = 4 - GameController.gameController.Inventory.Ingemones.Count;
            Mensajes.singleton.Popup("Para empezar a jugar debes comprar " + count + " Ingemones, presiona alguno de los botones de 'Comprar' para hacerlo");
            GameController.gameController.firstTime = true;
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
