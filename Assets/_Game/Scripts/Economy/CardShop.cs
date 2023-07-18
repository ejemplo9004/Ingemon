using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardShop : ItemShop
{
    public IEnumerator BuyCardSetCourutine(CardSet cardSet, int currentMoney)
    {
        imLoading.SetActive(true);
        UpdatePlayerMoney(currentMoney - cardSet.ShopCost);
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => !server.ocupado);
        yield return new WaitUntil(() => moneySubstracter.Done);
        UpdatePlayerCards(cardSet);
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => !server.ocupado);
        moneySubstracter.Done = false;
        imLoading.SetActive(false);
    }
    
    public void BuyDefaultCards(List<CardSet> defaultCardSets)
    {
        imLoading.SetActive(true);
        StartCoroutine(UpdatePlayerCardSets(defaultCardSets));
    }

    private void UpdatePlayerCards(CardSet cardSet)
    {
        var cards = cardSet.GetSetToSell();
        var data = new string[3];
        data[0] = GameController.gameController.usuarioActual.id.ToString();
        foreach (var card in cards)
        {
            data[1] = card.id;
            data[2] = "1";
            StartCoroutine(server.ConsumirServicio("crear carta usuario", data, GetServiceResponse));
            Debug.Log("Se compro la carta " + card.cardName);
        }
    }

    private IEnumerator UpdatePlayerCardSets(List<CardSet> cardSets)
    {
        var data = new string[3];
        data[0] = GameController.gameController.usuarioActual.id.ToString();
        foreach (var cardSet in cardSets)
        {
            foreach (var card in cardSet.Get())
            {
                data[1] = card.id;
                data[2] = "1";
                StartCoroutine(server.ConsumirServicio("crear carta usuario", data, GetServiceResponse));
                yield return new WaitUntil(() => !server.ocupado);
                Debug.Log("Se compro la carta " + card.cardName);
            }
        }
        imLoading.SetActive(false);
    }

}
