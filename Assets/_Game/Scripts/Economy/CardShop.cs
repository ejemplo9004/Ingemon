using System.Collections;
using UnityEngine;

public class CardShop : ItemShop
{
    public IEnumerator BuyCardSetCourutine(CardSet cardSet)
    {
        imLoading.SetActive(true);
        UpdatePlayerMoney(cardSet.ShopCost);
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => !server.ocupado);
        yield return new WaitUntil(() => moneySubstracter.Done);
        UpdatePlayerCards(cardSet);
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => !server.ocupado);
        moneySubstracter.Done = false;
        imLoading.SetActive(false);
    }

    private void UpdatePlayerCards(CardSet cardSet)
    {
        var cards = cardSet.GetSetToSell();
        var data = new string[2];
        data[0] = GameController.gameController.usuarioActual.id.ToString();
        foreach (var card in cards)
        {
            data[1] = card.id;
            //Consumir servicio
            Debug.Log("Se compro la carta " + card.cardName);
        }
    }
}
