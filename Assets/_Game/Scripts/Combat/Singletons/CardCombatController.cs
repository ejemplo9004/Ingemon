using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cards;
using UnityEngine;

public class CardCombatController: MonoBehaviour
{
    public List<Card> combatCards;
    private int index;

    public (List<Card>, List<Card>) Init(IngemonController frontAlly, IngemonController backAlly,
        EnemyController frontEnemy, EnemyController backEnemy)
    {
        index = 0;
        combatCards = new List<Card>();
        List<Card> allyCards = new List<Card>();
        List<Card> enemyCards = new List<Card>();
        
        foreach (var newCard in frontAlly.ingemonInfo.deck.Select(card => Card(card, frontAlly)))
        {
            combatCards.Add(newCard);
            allyCards.Add(newCard);
        }
        foreach (var newCard in backAlly.ingemonInfo.deck.Select(card => Card(card, backAlly)))
        {
            combatCards.Add(newCard);
            allyCards.Add(newCard);
        }
        foreach (var newCard in frontEnemy.ingemonInfo.deck.Select(card => Card(card, frontEnemy)))
        {
            combatCards.Add(newCard);
            enemyCards.Add(newCard);
        }
        foreach (var newCard in backEnemy.ingemonInfo.deck.Select(card => Card(card, backEnemy)))
        {
            combatCards.Add(newCard);
            enemyCards.Add(newCard);
        }
        
        return (allyCards, enemyCards);
    }

    public void AddCard(ScriptableCard card, EntityController owner)
    {
        combatCards.Add(Card(card, owner));
    }

    private Card Card(ScriptableCard info, EntityController owner)
    {
        return new Card(index++, info, owner);
    }

}
