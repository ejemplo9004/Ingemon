using System.Collections.Generic;
using UnityEngine;


public class EnemyActions
{
    public CombatInfo info;
    public int enemyEnergy = 3;
    public List<Card> enemyTurnPlays;
    public List<Card> discardEnemyDeck;

    public EnemyActions(CombatInfo info)
    {
        this.info = info;
        discardEnemyDeck = new List<Card>();
    }

    public void PrepareTurn()
    {
        enemyEnergy = 3;
        enemyTurnPlays = new List<Card>();
        while (enemyEnergy > 0)
        {
            Debug.Log($"Enemy energy {enemyEnergy}");
            Card card = FindNextCard(enemyEnergy);
            if (card == null)
            {
                return;
            }
            enemyTurnPlays.Add(card);
        }
        CombatSingletonManager.Instance.eventManager.EnemyIntentions(enemyTurnPlays);
    }

    //Encuentra la siguiente carta con costo menor al indicado.
    private Card FindNextCard(int cost)
    {
        if (info.enemyDeck.Count == 0)
        {
            foreach (var c in discardEnemyDeck)
            {
                info.enemyDeck.Add(c);
            }

            discardEnemyDeck = new List<Card>();
        }
        int index = 0;
        Debug.Log($"El deck enemigo tiene {info.enemyDeck.Count}");
        while (info.enemyDeck[index].info.cost > cost )
        {
            index++;
            Debug.Log($"Index : {index}");
            if (index == info.enemyDeck.Count)
            {
                return null;
            }
        }
        Card card = info.enemyDeck[index];
        info.enemyDeck.RemoveAt(index);
        enemyEnergy -= card.info.cost;
        return card;
    }

    public void PlayTurn()
    {
        foreach (var card in enemyTurnPlays)
        {
            card.info.PlayCard(card.owner);
            CombatSingletonManager.Instance.eventManager.ValidCardPlayed(card);
            discardEnemyDeck.Add(card);
            card.owner.TickBleed();
        }
    }
    
    
}