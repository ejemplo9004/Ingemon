using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cards;
using UnityEngine;

public class IngemonDeckManager : MonoBehaviour
{
    private Dictionary<ScriptableCard, int> availableUserCards = new Dictionary<ScriptableCard, int>();
    private List<ScriptableCard> userCards = new List<ScriptableCard>();
    [SerializeField] private Servidor server;
    private void Start()
    {
        availableUserCards = GameController.gameController.CardInventory.AvailablePlayerCards;
        userCards = GameController.gameController.CardInventory.PlayerCards.Keys.ToList();
    }

    public List<ScriptableCard> GetAllUserCards()
    {
        return userCards;
    }

    public List<ScriptableCard> GetAllUserCardsByType(IngemonRace race)
    {
        var cardsByRace = new List<ScriptableCard>();
        foreach (var card in userCards)
        {
            if (card.race == race)
            {
                cardsByRace.Add(card);
            }
        }

        return cardsByRace;
    }

    public Dictionary<ScriptableCard, int> GetAvailableUserCards()
    {
        return availableUserCards;
    }

    public Dictionary<ScriptableCard, int> GetAvailableUserCardsByRace(IngemonRace race)
    {
        var cardsByRace = new Dictionary<ScriptableCard, int>();
        foreach (var card in availableUserCards.Keys)
        {
            if (availableUserCards[card] > 0 && card.race == race)
            {
                cardsByRace.Add(card, availableUserCards[card]);
            }
        }

        return cardsByRace;
    }

    public void AddCardToIngemon(Ingemonster ingemon, ScriptableCard card)
    {
        ingemon.deck.Add(card);
        availableUserCards[card] -= 1;
        if (ingemon.deck.Count == 6) StartCoroutine(UpdateIngemon(ingemon));
    }

    public void DeleteCardFromIngemon(Ingemonster ingemon, ScriptableCard card)
    {
        if (ingemon.deck.Contains(card))
        {
            ingemon.deck.Remove(card);
            availableUserCards[card] += 1;
        }
    }

    public void ReturnIngemonCardsToAvailable(Ingemonster ingemon)
    {
        foreach (var card in ingemon.deck)
        {
            availableUserCards[card] += 1;
        }
    }

    private IEnumerator UpdateIngemon(Ingemonster ingemon)
    {
        var data = new string[7];
        data[0] = ingemon.id;
        data[1] = ingemon.deck[0].id;
        data[2] = ingemon.deck[1].id;
        data[3] = ingemon.deck[2].id;
        data[4] = ingemon.deck[3].id;
        data[5] = ingemon.deck[4].id;
        data[6] = ingemon.deck[5].id;

        StartCoroutine(server.ConsumirServicio("actualizar ingemon", data, PostIngemonUpdate));

        yield return new WaitUntil(() => !server.ocupado);
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
}
