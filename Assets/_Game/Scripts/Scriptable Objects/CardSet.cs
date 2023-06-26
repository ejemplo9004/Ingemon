using System.Collections.Generic;
using Cards;
using UnityEngine;

[CreateAssetMenu(fileName = "CardSet", menuName = "Ingemon/CardSet")]
public class CardSet : ScriptableObject
{
    [SerializeField] private List<ScriptableCard> cards;
    [SerializeField] private int shopCost;
    [SerializeField] private int cardsAmountToSell;
    public int ShopCost => shopCost;

    public List<ScriptableCard> Get()
    {
        return cards;
    }

    public List<ScriptableCard> GetSetToSell()
    {
        var setToSell = new List<ScriptableCard>();
        for (var i = 0; i < cardsAmountToSell; i++)
        {
            var index = Random.Range(0, cards.Count);
            setToSell.Add(cards[index]);
        }

        return setToSell;
    }
}