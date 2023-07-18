using System.Collections.Generic;
using UnityEngine;

public class ShopFacade : MonoBehaviour
{
    [SerializeField] private PlayerEconomy playerEconomy;
    [SerializeField] private IngemonShop ingemonShop;
    [SerializeField] private CardShop cardShop;

    public void BuyIngemon(int playerMoney, Ingemonster newIngemon)
    {
        StartCoroutine(ingemonShop.BuyIngemonCoroutine(playerMoney, newIngemon));
    }

    public void BuyCardSet(CardSet cardSet)
    {
        if (playerEconomy.VerifyBuy(cardSet.ShopCost))
        {
            StartCoroutine(cardShop.BuyCardSetCourutine(cardSet, playerEconomy.money));
        }
    }
    
    public void BuyCardSet(List<CardSet> cardSets)
    {
        cardShop.BuyDefaultCards(cardSets);
    }
}
