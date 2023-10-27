using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IngemonShop : ItemShop
{
    [SerializeField] private CardShop cardShop;
    private Ingemonster _searchedIngemon;
    public IEnumerator BuyIngemonCoroutine(int playerMoney, Ingemonster newIngemon)
    {
        imLoading.SetActive(true);
        UpdatePlayerMoney(playerMoney);
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => !server.ocupado);
        yield return new WaitUntil(() => moneySubstracter.Done);
        AddIngemonToPlayer(newIngemon);
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => !server.ocupado);
        moneySubstracter.Done = false;
        imLoading.SetActive(false);
    }

    private void AddIngemonToPlayer(Ingemonster newIngemon)
    {
        var data = new string[4];
        data[0] = newIngemon.name;
        data[1] = newIngemon.phenotype;
        data[2] = newIngemon.maxHealth.ToString();
        data[3] = GameController.gameController.usuarioActual.id.ToString();
        StartCoroutine(server.ConsumirServicio("guardar ingemon", data, GetServiceResponse));
        cardShop.BuyDefaultCards(GameController.gameController.CardInventory.DefaultCardSets);
        StartCoroutine(AssignDefaultCards(newIngemon));
    }

    private IEnumerator AssignDefaultCards(Ingemonster newIngemon)
    {
        yield return new WaitUntil(() => !server.ocupado);
        string[] feat = newIngemon.phenotype.Split("-");
        int race = Int32.Parse(feat[6]);
        var cardSets = GameController.gameController.CardInventory.DefaultCardSets;
        switch (race)
        {
            case 0:
                AddCards(newIngemon, cardSets[2]);
                break;
            case 1:
                AddCards(newIngemon, cardSets[1]);
                break;
            case 2:
                AddCards(newIngemon, cardSets[3]);
                break;
            case 3:
                AddCards(newIngemon, cardSets[0]);
                break;
            default:
                break;
        }
    }

    private void AddCards(Ingemonster newIngemon, CardSet cardset)
    {
        foreach(var card in cardset.Get())
        {
            newIngemon.deck.Add(card);
            if (newIngemon.deck.Count == 6) break;
        }
        StartCoroutine(UpdateIngemon(newIngemon));
    }

    private IEnumerator UpdateIngemon(Ingemonster ingemon)
    {
        var datos = new string[1];
        datos[0] = GameController.gameController.usuarioActual.id.ToString();
        yield return new WaitUntil(() => !server.ocupado);
        StartCoroutine(server.ConsumirServicio("buscar ingemon", datos, PosBuscarIngemon));
        yield return new WaitUntil(() => !server.ocupado);
        var data = new string[7];
        data[0] = _searchedIngemon.id;
        data[1] = ingemon.deck[0] != null ? ingemon.deck[0].id : string.Empty;
        data[2] = ingemon.deck[1] != null ? ingemon.deck[1].id : string.Empty;
        data[3] = ingemon.deck[2] != null ? ingemon.deck[2].id : string.Empty;
        data[4] = ingemon.deck[3] != null ? ingemon.deck[3].id : string.Empty;
        data[5] = ingemon.deck[4] != null ? ingemon.deck[4].id : string.Empty;
        data[6] = ingemon.deck[5] != null ? ingemon.deck[5].id : string.Empty;

        Debug.Log(String.Join(", ", data));

        StartCoroutine(server.ConsumirServicio("actualizar ingemon", data, PostIngemonUpdate));

        yield return new WaitUntil(() => !server.ocupado);
        Debug.Log($"cartas de ingemon {ingemon.name} actualizadas");
    }

    private void PostIngemonUpdate()
    {
        switch (server.respuesta.codigo)
        {
            case 220:
                Debug.Log("Ingemon actualizado correctamente");
                break;
            case 501:
                Debug.Log("Error actualizando ingemon");
                break;
            case 404:
                Debug.Log("Paila");
                break;
            case 500:
                Debug.Log("Error al buscar ingemon");
                break;
        }
    }

    void PosBuscarIngemon()
    {
        switch (server.respuesta.codigo)
        {
            case 210: //ingemon encontrado
                List<string> ingemones = server.respuesta.respuesta.Split("!").ToList();
                ingemones.Remove("");
                _searchedIngemon = JsonUtility.FromJson<Ingemonster>(ingemones[ingemones.Count - 1]);
                break;
            case 404: // Error
                Mensajes.singleton.Popup("Error, no se puede conectar con el servidor");
                Debug.Log(server.respuesta.mensaje);
                break;
            case 402: // faltan datos para ejecutar la accion solicitada
                Debug.Log("locoo");
                break;
            case 410: // ingemones no encontrados
                //GameController.gameController.firstTime = true;
                Debug.Log("locoo4");
                break;
            default:
                break;
        }
    }
}