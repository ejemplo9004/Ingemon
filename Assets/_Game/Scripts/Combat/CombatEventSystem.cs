using System.Collections;
using System.Collections.Generic;
using Cards;
using UnityEngine;

public class CombatEventSystem : MonoBehaviour
{
    public static CombatEventSystem Instance { get; private set; }

    #region Singleton

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    #endregion

    public delegate void OnEndButtonAction();
    public event OnEndButtonAction OnEndClicked;

    public delegate void OnCardPlayedAction(ScriptableCard card);
    public event OnCardPlayedAction OnCardPlayed;

    public delegate void OnEnergyAction();
    public event OnEnergyAction OnEnergyChange;

    public delegate void OnCardChangeAction(ScriptableCard card);
    public event OnCardChangeAction OnCardChange;

    public delegate void OnHandUpdateAction(List<ScriptableCard> cards);
    public event OnHandUpdateAction OnHandUpdate;

    public delegate void OnCardDiscardAction(ScriptableCard card);
    public event OnCardDiscardAction OnCardDiscard;

    public void EndTurnButton() => OnEndClicked?.Invoke();

    public void ChangeEnergy() => OnEnergyChange?.Invoke();

    public void UpdateCardHand(ScriptableCard card) => OnCardChange?.Invoke(card);

    public void UpdateHand(List<ScriptableCard> cards) => OnHandUpdate?.Invoke(cards);

    public void DiscardCard(ScriptableCard card) => OnCardDiscard?.Invoke(card);

    public void PlayCard(ScriptableCard card)
    {
        OnCardPlayed?.Invoke(card);
    }
}
