using System.Collections.Generic;
using Cards;
using UnityEngine;

public class IngemonDeckManager : MonoBehaviour
{
    private Dictionary<ScriptableCard, int> availableUserCards = new Dictionary<ScriptableCard, int>();
    private List<ScriptableCard> userCards = new List<ScriptableCard>();
    private void Start()
    {
        availableUserCards = GameController.gameController.UserCardsAmount;
        userCards = GameController.gameController.CardInventory.BaseCollection;
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
    }

    public void DeleteCardFromIngemon(Ingemonster ingemon, ScriptableCard card)
    {
        if (ingemon.deck.Contains(card))
        {
            ingemon.deck.Remove(card);
            availableUserCards[card] += 1;
        }
    }
}
