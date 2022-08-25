using System;
using System.Collections;
using System.Collections.Generic;
using Cards;
using UnityEngine;

public class CombatEventSystem : MonoBehaviour
{
    public delegate void OnEndButtonAction();
    public event OnEndButtonAction OnEndClicked;

    public delegate void OnCardPlayedAction(Card card);
    public event OnCardPlayedAction OnCardPlayed;
    public event OnCardPlayedAction OnValidCardPlayed;

    public delegate void OnUIChangeAction();
    public event OnUIChangeAction OnEnergyChange;
    public event OnUIChangeAction OnHealthChange;
    public event OnUIChangeAction OnIntentionsChange;

    public delegate void OnCardChangeAction(Card card);
    public event OnCardChangeAction OnCardChange;

    public delegate void OnCardsAction(List<Card> cards);
    public event OnCardsAction OnHandUpdate;
    public event OnCardsAction OnEnemyIntentions;

    public delegate void OnCardDiscardAction(Card card);
    public event OnCardDiscardAction OnCardDiscard;

    public void EndTurnButton() => OnEndClicked?.Invoke();

    public void ChangeEnergy() => OnEnergyChange?.Invoke();

    public void ChangeHealth() => OnHealthChange?.Invoke();

    public void UpdateCardHand(Card card) => OnCardChange?.Invoke(card);

    public void UpdateHand(List<Card> cards) => OnHandUpdate?.Invoke(cards);

    public void DiscardCard(Card card) => OnCardDiscard?.Invoke(card);

    public void EnemyIntentions(List<Card> cards) => OnEnemyIntentions?.Invoke(cards);
    public void IntentionsClean() => OnIntentionsChange?.Invoke();

    public void PlayCard(Card card)
    {
        OnCardPlayed?.Invoke(card);
    }

    public void ValidCardPlayed(Card card)
    {
        OnValidCardPlayed?.Invoke(card);
    }
}
