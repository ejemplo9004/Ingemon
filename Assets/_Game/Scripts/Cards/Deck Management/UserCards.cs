using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UserCards : MonoBehaviour
{
    [SerializeField] private Servidor server;
    private void Start()
    {
        RestartUserCards();
        StartCoroutine(GetUserCards());
    }

    private void RestartUserCards()
    {
        GameController.gameController.CardInventory.PlayerCards.Clear();
        GameController.gameController.CardInventory.AvailablePlayerCards.Clear();
    }

    private IEnumerator GetUserCards()
    {
        string[] data = new string[1];
        data[0] = GameController.gameController.usuarioActual.id.ToString();
        StartCoroutine(server.ConsumirServicio("buscar cartas usuario", data, PostSearchPlayerCards));
        yield return new WaitUntil(() => !server.ocupado);
    }
    
    private void PostSearchPlayerCards()
    {
        switch (server.respuesta.codigo)
        {
            case 214: //user cards found 220 melo, 501 error actualizar inge, 404 no encontro, 500 error al buscar ingemon
                List<string> cards = server.respuesta.respuesta.Split('!').ToList();
                SetPlayerCards(cards);
                break;
            case 410:
                break;
        }
    }

    private void SetPlayerCards(List<string> cardsInfo)
    {
        char[] delimiters = { ':', ',' };
        foreach (var cardInfo in cardsInfo)
        {
            var cardInfoSplit = cardInfo.Split(delimiters).ToList();

            if (cardInfoSplit.Count < 4)
            {
                SetAvailablePlayerCards();
                return;
            }
            var cardId = cardInfoSplit[3];
            var card = GameController.gameController.CardInventory.AllCardsDictionary[cardId];
            var amount = Int32.Parse(cardInfoSplit[5]);
            
            GameController.gameController.CardInventory.AddCard(card, amount);
        }
    }

    private void SetAvailablePlayerCards()
    {
        var ingemones = GameController.gameController.Inventory.Ingemones;
        var cardInventory = GameController.gameController.CardInventory;
        foreach (var ingemon in ingemones)
        {
            if (ingemon.id_carta1 != 0)
            {
                ingemon.FillDeck(cardInventory.GetCard(ingemon.id_carta1.ToString()));
                cardInventory.ModifyAvailablePlayerCards(ingemon.id_carta1.ToString(), false);
            }
            if (ingemon.id_carta2 != 0)
            {
                ingemon.FillDeck(cardInventory.GetCard(ingemon.id_carta2.ToString()));
                cardInventory.ModifyAvailablePlayerCards(ingemon.id_carta2.ToString(), false);
            }
            if (ingemon.id_carta3 != 0)
            {
                ingemon.FillDeck(cardInventory.GetCard(ingemon.id_carta3.ToString()));
                cardInventory.ModifyAvailablePlayerCards(ingemon.id_carta3.ToString(), false);
            }
            if (ingemon.id_carta4 != 0)
            {
                ingemon.FillDeck(cardInventory.GetCard(ingemon.id_carta4.ToString()));
                cardInventory.ModifyAvailablePlayerCards(ingemon.id_carta4.ToString(), false);
            }
            if (ingemon.id_carta5 != 0)
            {
                ingemon.FillDeck(cardInventory.GetCard(ingemon.id_carta5.ToString()));
                cardInventory.ModifyAvailablePlayerCards(ingemon.id_carta5.ToString(), false);
            }
            if (ingemon.id_carta6 != 0)
            {
                ingemon.FillDeck(cardInventory.GetCard(ingemon.id_carta6.ToString()));
                cardInventory.ModifyAvailablePlayerCards(ingemon.id_carta6.ToString(), false);
            }
        }
    }
}
