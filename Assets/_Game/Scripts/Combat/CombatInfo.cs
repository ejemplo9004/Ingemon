using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cards;
using UnityEngine;

public class CombatInfo
{
    public IngemonController frontAlly;
    public IngemonController backAlly;
    public EnemyController frontEnemy;
    public EnemyController backEnemy;
    public List<Card> enemyDeck;
    public List<Card> drawDeck;
    public List<Card> discardDeck;
    public List<Card> hand;
    public int currentEnergy, maxEnergy;
    public Vector3 frontAllyPos, backAllyPos, frontEnemyPos, backEnemyPos;

    public CombatInfo(IngemonController frontAlly, IngemonController backAlly,
        EnemyController frontEnemy, EnemyController backEnemy)
    {
        this.frontAlly = frontAlly;
        this.backAlly = backAlly;
        this.frontEnemy = frontEnemy;
        this.backEnemy = backEnemy;
        frontAllyPos = Vector3.zero;
        backAllyPos = frontAllyPos + new Vector3(5, 0, 0);
        frontEnemyPos = backAllyPos + new Vector3(0, 0, 10);
        backEnemyPos = frontEnemyPos + new Vector3(5, 0, 0);
        currentEnergy = 3;
        maxEnergy = 3;
        hand = new List<Card>();
        discardDeck = new List<Card>();
    }

    public void SpawnAllys()
    {
        frontAlly.Spawn(frontAllyPos);
        backAlly.Spawn(backAllyPos);
    }

    public void SpawnEnemies()
    {
        frontEnemy.Spawn(frontEnemyPos);
        backEnemy.Spawn(backEnemyPos);
    }

    public bool IsPlayable(Card card) => currentEnergy >= card.info.cost;

    public void PlayCard(Card card)
    {
        if (IsPlayable(card))
        {
            SpendEnergy(card.info.cost);
            card.info.PlayCard();
            CombatSingletonManager.Instance.eventManager.DiscardCard(card);
        }
        else
            Debug.Log("No enough energy");
    }

    public void SpendEnergy(int spent)
    {
        currentEnergy -= spent;
        CombatSingletonManager.Instance.eventManager.ChangeEnergy();
    }

    public void ResetEnergy()
    {
        currentEnergy = maxEnergy;
        CombatSingletonManager.Instance.eventManager.ChangeEnergy();
    }

    public void Draw(int draws)
    {
        for (int i = 0; i < draws; i++)
        {
            if (drawDeck.Count > 0)
            {
                if (hand.Count >= 10)
                {
                    discardDeck.Add(drawDeck[0]);
                    drawDeck.RemoveAt(0);
                }
                else
                {
                    AddToHand(drawDeck[0]);
                    drawDeck.RemoveAt(0);
                }
            }
            else
            {
                FillDeck();
                i--;
                if (drawDeck.Count == 0)
                {
                    Debug.Log("La deck esta empty");
                    return;
                }
            }
        }
    }


    public void ShuffleDeck()
    {
        int n = drawDeck.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n);
            (drawDeck[k], drawDeck[n]) = (drawDeck[n], drawDeck[k]);
        }
    }

    public void FillDeck()
    {
        while (discardDeck.Count > 0)
        {
            drawDeck.Add(discardDeck[0]);
            discardDeck.RemoveAt(0);
        }

        ShuffleDeck();
    }

    public void LogDeck()
    {
        foreach (var card in drawDeck)
        {
            Debug.Log($"{card}");
        }
    }

    public void AddToHand(Card card)
    {
        Debug.Log($"Spawning Card {card.id} : {card.info.cardName}");
        hand.Add(card);
        CombatSingletonManager.Instance.eventManager.UpdateCardHand(card);
    }

    public void Discard(Card card)
    {
        discardDeck.Add(card);
        hand.Remove(card);
    }

    public void DiscardHand()
    {
        while (hand.Count > 0)
        {
            Discard(hand[0]);
        }

        CombatSingletonManager.Instance.eventManager.UpdateHand(hand);
    }
}