using System.Collections.Generic;
using UnityEngine;
using Cards;

[CreateAssetMenu(fileName = "Deck Inventory", menuName = "Ingemon/Deck Inventory")]
public class CardInventory : ScriptableObject
{
    private Dictionary<ScriptableCard, int> playerCards = new Dictionary<ScriptableCard, int>();
    private Dictionary<ScriptableCard, int> availablePlayerCards = new Dictionary<ScriptableCard, int>();
    private Dictionary<string, ScriptableCard> allCardsDictionary = new Dictionary<string, ScriptableCard>();
    [SerializeField] private List<ScriptableCard> allCards;
    [SerializeField] private List<CardSet> defaultCardSets;

    public void AddCard(ScriptableCard card, int amount)
    {
        playerCards.Add(card, amount);
        AvailablePlayerCards.Add(card, amount);
    }

    public void ModifyAvailablePlayerCards(string cardId, bool add)
    {
        var card = allCardsDictionary[cardId];
        if(AvailablePlayerCards[card] == 0 && !add) return;
        AvailablePlayerCards[card] = add ? AvailablePlayerCards[card] + 1 : AvailablePlayerCards[card] - 1;
    }
    public void RemoveCard(ScriptableCard card)
    {
        if (playerCards.ContainsKey(card))
        {
            playerCards.Remove(card);
        }
    }

    public void GenerateCardDictionary()
    {
        foreach (var card in allCards)
        {
            AllCardsDictionary.Add(card.id, card);
        }
    }

    public void ClearCardDictionary()
    {
        allCardsDictionary.Clear();
    }

    public ScriptableCard GetRandomCard()
    {
        int index = Random.Range(0, allCards.Count);
        return allCards[index];
    }

    public ScriptableCard GetCard(string id)
    {
        return allCardsDictionary[id];
    }

    public Dictionary<ScriptableCard, int> PlayerCards => playerCards;
    public List<ScriptableCard> AllCards => allCards;
    public List<CardSet> DefaultCardSets => defaultCardSets;
    public Dictionary<string, ScriptableCard> AllCardsDictionary => allCardsDictionary;

    public Dictionary<ScriptableCard, int> AvailablePlayerCards => availablePlayerCards;
}
