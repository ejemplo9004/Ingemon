using UnityEngine;

public abstract class ItemShop : MonoBehaviour
{
    [SerializeField] protected Servidor server;
    [SerializeField] protected GameObject imLoading;
    [SerializeField] protected PlayerMoney moneySubstracter;
    protected void UpdatePlayerMoney(int value)
    {
        StartCoroutine(moneySubstracter.AddMoneyCoroutine(value, true));
    }
    
    protected void GetServiceResponse()
    {
        switch (server.respuesta.codigo)
        {
            case 209: //jugador editado correctamente 
                print(server.respuesta.mensaje);
                break;
            case 213: //carta anadida correctamente
                print(server.respuesta.mensaje);
                break;
            case 215: //carta mela
                print(server.respuesta.codigo);
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
}
