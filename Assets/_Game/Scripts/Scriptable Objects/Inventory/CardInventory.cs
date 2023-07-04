using System.Collections.Generic;
using UnityEngine;
using Cards;

[CreateAssetMenu(fileName = "Deck Inventory", menuName = "Ingemon/Deck Inventory")]
public class CardInventory : ScriptableObject
{
    [SerializeField] private List<ScriptableCard> playerCards;
    [SerializeField] private List<ScriptableCard> allCards;
    [SerializeField] private List<CardSet> defaultCardSets;

    public void AddCard(ScriptableCard card)
    {
        playerCards.Add(card);
    }

    public void RemoveCard(ScriptableCard card)
    {
        if (playerCards.Contains(card))
        {
            int index = playerCards.IndexOf(card);
            playerCards.RemoveAt(index);
        }
    }

    public ScriptableCard GetRandomCard()
    {
        int index = Random.Range(0, allCards.Count);
        return allCards[index];
    }

    public List<ScriptableCard> PlayerCards => playerCards;
    public List<ScriptableCard> AllCards => allCards;
    public List<CardSet> DefaultCardSets => defaultCardSets;
}
