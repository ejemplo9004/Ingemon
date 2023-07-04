using System.Collections;
using Cards;
using UnityEngine;

public class CardGenerator : MonoBehaviour
{
    [SerializeField] private Economy gameEconomy;
    [SerializeField] private PlayerEconomy playerEconomy;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private GameObject cardParent;
    [SerializeField] private float timeToShowDeck;
    private bool isInCreation;
    
    public void CreateCard()
    {
        if (isInCreation) return;
        int cardCost = gameEconomy.GetItemCost("Card");
        if (playerEconomy.VerifyBuy(cardCost))
        {
            // ScriptableCard card = GameController.gameController.CardInventory.GetRandomCard();
            // GameController.gameController.CardInventory.AddCard(card);
            // StartCoroutine(ShowCard(card));
            // isInCreation = true;
        }
    }

    public IEnumerator ShowCard(ScriptableCard cardSO)
    {
        Card card = new Card(cardSO);
        GameObject cardCopy = Instantiate(cardPrefab, cardParent.transform);
        cardCopy.GetComponent<CardSpriteController>().InitCardSprite(card);
        yield return new WaitForSeconds(timeToShowDeck);
        Destroy(cardCopy);
        isInCreation = false;
    }

    public bool IsInCreation => isInCreation;
}
