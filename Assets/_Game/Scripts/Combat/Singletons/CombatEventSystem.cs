using System;
using System.Collections;
using System.Collections.Generic;
using Cards;
using UnityEngine;

public class CombatEventSystem : MonoBehaviour
{
    #region UIEvents

    public delegate void OnEndButtonAction();

    public event OnEndButtonAction OnEndClicked;
    public void EndTurnButton() => OnEndClicked?.Invoke();

    public delegate void OnUIChangeAction();

    public event OnUIChangeAction OnEnergyChange;
    public event OnUIChangeAction OnHealthChange;
    public event OnUIChangeAction OnIntentionsChange;

    public void ChangeEnergy() => OnEnergyChange?.Invoke();

    public void ChangeHealth() => OnHealthChange?.Invoke();
    public void IntentionsClean() => OnIntentionsChange?.Invoke();

    #endregion

    #region CardEvents

    public delegate void OnCardPlayedAction(Card card);

    public event OnCardPlayedAction OnCardPlayed;
    public event OnCardPlayedAction OnValidCardPlayed;


    public delegate void OnCardChangeAction(Card card);

    public event OnCardChangeAction OnCardChange;

    public delegate void OnCardsAction(List<Card> cards);

    public event OnCardsAction OnHandUpdate;
    public event OnCardsAction OnEnemyIntentions;

    public delegate void OnCardDiscardAction(Card card);

    public event OnCardDiscardAction OnCardDiscard;

    public void UpdateCardHand(Card card) => OnCardChange?.Invoke(card);

    public void UpdateHand(List<Card> cards) => OnHandUpdate?.Invoke(cards);

    public void DiscardCard(Card card) => OnCardDiscard?.Invoke(card);

    public void EnemyIntentions(List<Card> cards) => OnEnemyIntentions?.Invoke(cards);

    public void PlayCard(Card card)
    {
        OnCardPlayed?.Invoke(card);
    }

    public void ValidCardPlayed(Card card)
    {
        OnValidCardPlayed?.Invoke(card);
    }

    #endregion

    #region IngemonEvents

    public delegate void OnIngemonDeadAction(EntityController ingemon);
    public event OnIngemonDeadAction OnIngemonDead;

    public delegate void OnEndBattleAction();

    public event OnEndBattleAction OnWinBattle;
    public event OnEndBattleAction OnFailBattle;
    public void DeadIngemon(EntityController ingemon) => OnIngemonDead?.Invoke(ingemon);
    public void WinBattle() => OnWinBattle?.Invoke();
    public void FailedBattle() => OnFailBattle?.Invoke();

    #endregion
}