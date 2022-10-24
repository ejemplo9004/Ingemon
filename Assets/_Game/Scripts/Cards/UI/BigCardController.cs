using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BigCardController : MonoBehaviour
{
    [SerializeField] private CardSpriteController bigCard;
    [SerializeField] private float duration = 1;
    private Queue<Card> showQueue;
    private bool isRunning;
    private WaitForSeconds wait;

    private void Awake()
    {
        showQueue = new Queue<Card>();
        wait = new WaitForSeconds(duration);
    }

    public void AddToShow(Card card)
    {
        showQueue.Enqueue(card);
        if (isRunning) return;
        StartCoroutine(ShowCardsCoroutine());
    }

    public void SetCard(Card card)
    {
        bigCard.InitCardSprite(card);
    }

    private IEnumerator ShowCardsCoroutine()
    {
        isRunning = true;
        while (showQueue.Count > 0)
        {
            SetCard(showQueue.Dequeue());
            bigCard.gameObject.SetActive(true);
            yield return wait;
            bigCard.gameObject.SetActive(false);
        }

        isRunning = false;
        yield return null;
    }

    private void OnDisable()
    {
        StopCoroutine(ShowCardsCoroutine());
        isRunning = false;
        showQueue.Clear();
    }
}