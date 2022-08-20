using System;
using System.Collections;
using System.Collections.Generic;
using Cards;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    private List<GameObject> cardObjects;

    public void Awake()
    {
        cardObjects = new List<GameObject>();
        Debug.Log($"Empezando a suscribirse");
        CombatSingletonManager.Instance.eventManager.OnCardChange += RenderCard;
        CombatSingletonManager.Instance.eventManager.OnHandUpdate += UpdateHand;
        CombatSingletonManager.Instance.eventManager.OnCardDiscard += DiscardCard;
        Debug.Log($"Suscrito a eventos RenderCard");
    }

    private void RenderCard(Card card)
    {
        var cardRenderer = Instantiate(cardPrefab, gameObject.transform);
        CardSpriteController controller = cardRenderer.GetComponent<CardSpriteController>();
        controller.InitCardSprite(card);
        cardObjects.Add(cardRenderer);
    }

    private void DiscardCard(Card card)
    {
        int pos = cardObjects.FindIndex(x => x.GetComponent<CardSpriteController>().cardId == card.id);
        GameObject c = cardObjects[pos];
        cardObjects.RemoveAt(pos);
        Destroy(c.gameObject);
    }

    private void UpdateHand(List<Card> hand)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        cardObjects = new List<GameObject>();
        foreach (var card in hand)
        {
            RenderCard(card);
        }
    }
}