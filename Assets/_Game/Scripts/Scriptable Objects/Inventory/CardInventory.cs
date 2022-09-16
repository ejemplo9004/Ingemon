using System.Collections.Generic;
using UnityEngine;
using Cards;

[CreateAssetMenu(fileName = "Deck Inventory", menuName = "Ingemon/Deck Inventory")]
public class CardInventory : ScriptableObject
{
    [SerializeField] private List<ScriptableCard> baseCollection;
    [SerializeField] private List<ScriptableCard> allCards;

    public void AddCard(ScriptableCard card)
    {
        baseCollection.Add(card);
    }

    public void RemoveCard(ScriptableCard card)
    {
        if (baseCollection.Contains(card))
        {
            int index = baseCollection.IndexOf(card);
            baseCollection.RemoveAt(index);
        }
    }

    public ScriptableCard GetRandomCard()
    {
        int index = Random.Range(0, allCards.Count);
        return allCards[index];
    }

    public List<ScriptableCard> BaseCollection => baseCollection;
    public List<ScriptableCard> AllCards => allCards;
}
