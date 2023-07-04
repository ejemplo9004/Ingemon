using System.Collections;
using UnityEngine;

public class CardDbAdder : MonoBehaviour
{
    [SerializeField] private Servidor server;

    public void AddCardsToDB()
    {
        StartCoroutine(AddCardToDBCoroutine());
    }
    
    IEnumerator AddCardToDBCoroutine()
    {
        var data = new string[1];
        foreach (var card in GameController.gameController.CardInventory.AllCards)
        {
            data[0] = card.id;
            StartCoroutine(server.ConsumirServicio("crear carta", data, PostAddCard));
            yield return new WaitUntil(() => !server.ocupado);
        }
    }

    private void PostAddCard()
    {
        switch (server.respuesta.codigo)
        {
            case 215: //card created successfully 
                Logger.Instance.LogInfo(server.respuesta.mensaje);
                break;
            default:
                Logger.Instance.LogInfo(server.respuesta.mensaje);
                break;
        }
    }
}
