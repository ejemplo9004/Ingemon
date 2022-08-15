using System.Collections;
using System.Collections.Generic;
using Cards;
using UnityEngine;

public class CardCombatController: MonoBehaviour
{
    public List<Card> combatCards;
    private int index;
    public static CardCombatController Instance { get; private set; }
    
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

    public void Init(List<ScriptableCard> allyDeck, List<ScriptableCard> enemyDeck)
    {
        index = 0;
        combatCards = new List<Card>();
        foreach (var card in allyDeck)
        {
            combatCards.Add(new Card(index, card));
            index++;
        }
        foreach (var card in enemyDeck)
        {
            combatCards.Add(new Card(index, card));
            index++;
        }
    }

    public void AddCard(ScriptableCard card)
    {
        combatCards.Add(new Card(index, card));
    }

}
