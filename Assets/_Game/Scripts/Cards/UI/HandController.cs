using System;
using System.Collections;
using System.Collections.Generic;
using Cards;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    private List<GameObject> cardObjects;

    public void Start()
    {
        cardObjects = new List<GameObject>();
        CombatEventSystem.Instance.OnCardChange += RenderCard;
        CombatEventSystem.Instance.OnHandUpdate += UpdateHand;
        CombatEventSystem.Instance.OnCardDiscard += DiscardCard;
    }

    private void RenderCard(ScriptableCard card)
    {
        var cardRenderer = Instantiate(cardPrefab, gameObject.transform);
        CardSpriteController controller = cardRenderer.GetComponent<CardSpriteController>();
        controller.Card(card)
            .Titled(card.cardName)
            .WithDescription(card.cardDescription)
            .WithCost(card.cost);
        cardObjects.Add(cardRenderer);
    }

    private void DiscardCard(ScriptableCard card)
    {
        int pos = cardObjects.FindIndex(x => x.GetComponent<CardSpriteController>().card.Equals(card));
        GameObject c = cardObjects[pos];
        cardObjects.RemoveAt(pos);
        Destroy(c.gameObject);
    }

    private void UpdateHand(List<ScriptableCard> hand)
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