using System;
using System.Collections;
using System.Collections.Generic;
using Cards;
using UnityEngine;
public class HandController : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private float duration = 1;
    private List<GameObject> cardObjects;
    private Queue<Card> incomingCards;
    private bool isDrawing;
    private WaitForSeconds wait;
    public void OnEnable()
    {
        incomingCards = new Queue<Card>();
        wait = new WaitForSeconds(duration);
        cardObjects = new List<GameObject>();
        CombatSingletonManager.Instance.eventManager.OnCardChange += RenderCard;
        CombatSingletonManager.Instance.eventManager.OnHandUpdate += UpdateHand;
        CombatSingletonManager.Instance.eventManager.OnCardDiscard += DiscardCard;
    }
    public void OnDisable()
    {
        cardObjects = null;
        CombatSingletonManager.Instance.eventManager.OnCardChange -= RenderCard;
        CombatSingletonManager.Instance.eventManager.OnHandUpdate -= UpdateHand;
        CombatSingletonManager.Instance.eventManager.OnCardDiscard -= DiscardCard;
    }

    private void RenderCard(Card card)
    {
        var cardRenderer = Instantiate(cardPrefab, gameObject.transform);
        CardSpriteController controller = cardRenderer.GetComponent<CardSpriteController>();
        controller.InitCardSprite(card);
        cardObjects.Add(cardRenderer);
    }

    private void AddCard(Card card)
    {
        incomingCards.Enqueue(card);
        if(isDrawing) return;
        StartCoroutine(DrawCardCoroutine());
    }

    private IEnumerator DrawCardCoroutine()
    {
        isDrawing = true;
        while (incomingCards.Count > 0)
        {
            //AnimateCard(incomingCards.Dequeue())
            yield return wait;
        }

        isDrawing = false;
        yield return null;
    }

    private void DiscardCard(Card card)
    {
        int pos = cardObjects.FindIndex(x => x.GetComponent<CardSpriteController>().cardId == card.id);
        if (pos >= 0)
        {
            GameObject c = cardObjects[pos];
            cardObjects.RemoveAt(pos);
            Destroy(c.gameObject);
        }
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