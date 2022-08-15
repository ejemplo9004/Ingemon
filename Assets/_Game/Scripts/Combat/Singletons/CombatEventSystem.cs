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

    public delegate void OnEnergyAction();
    public event OnEnergyAction OnEnergyChange;

    public delegate void OnCardChangeAction(Card card);
    public event OnCardChangeAction OnCardChange;

    public delegate void OnHandUpdateAction(List<Card> cards);
    public event OnHandUpdateAction OnHandUpdate;

    public delegate void OnCardDiscardAction(Card card);
    public event OnCardDiscardAction OnCardDiscard;

    public void EndTurnButton() => OnEndClicked?.Invoke();

    public void ChangeEnergy() => OnEnergyChange?.Invoke();

    public void UpdateCardHand(Card card) => OnCardChange?.Invoke(card);

    public void UpdateHand(List<Card> cards) => OnHandUpdate?.Invoke(cards);

    public void DiscardCard(Card card) => OnCardDiscard?.Invoke(card);

    public void PlayCard(Card card)
    {
        OnCardPlayed?.Invoke(card);
    }
}
