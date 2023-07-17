using System.Collections;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    [SerializeField] private PlayerEconomy playerEconomy;
    [SerializeField] private Servidor server;
    private bool done;

    public IEnumerator AddMoneyCoroutine(int money, bool ack)
    {
        UpdatePlayerMoney(money, ack);
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => !server.ocupado);
    }

    private void UpdatePlayerMoney(int money, bool ack)
    {
        var data = new string[2];
        data[0] = GameController.gameController.usuarioActual.id.ToString();
        data[1] = money.ToString();
        StartCoroutine(server.ConsumirServicio("actualiza jugador", data, delegate { GetServiceResponse(ack); }));
    }
    
    private void GetServiceResponse(bool ack)
    {
        switch (server.respuesta.codigo)
        {
            case 209: //jugador editado correctamente 
                print(server.respuesta.mensaje);
                if (ack) Done = true;
                break;
            case 404: // Error
                print("Error, no se puede conectar con el servidor");
                break;
            case 402: // faltan datos para ejecutar la accion solicitada
                print(server.respuesta.mensaje);
                break;
            case 210: // jugador no existe
                print(server.respuesta.mensaje);
                break;
            case 408: // error creando el jugador
                print(server.respuesta.mensaje);
                break;
        }
    }
    public bool Done
    {
        get => done;
        set => done = value;
    }
}
