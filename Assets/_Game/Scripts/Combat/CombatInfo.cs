using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cards;
using UnityEngine;

public class CombatInfo
{
    public Inventory combatInventory;
    public IngemonController frontAlly;
    public IngemonController backAlly;
    public EnemyController frontEnemy;
    public EnemyController backEnemy;
    public List<Card> enemyDeck;
    public List<Card> drawDeck;
    public List<Card> discardDeck;
    public List<Card> hand;
    public HandHandler handler;
    public EnergyHandler energizer;
    public CardExecutioner executioner;
    
    public Vector3 frontAllyPos, backAllyPos, frontEnemyPos, backEnemyPos;

    public CombatInfo(IngemonController frontAlly, IngemonController backAlly,
        EnemyController frontEnemy, EnemyController backEnemy, Inventory combatInventory)
    {
        this.frontAlly = frontAlly;
        this.backAlly = backAlly;
        this.frontEnemy = frontEnemy;
        this.backEnemy = backEnemy;
        this.combatInventory = combatInventory;
        frontAllyPos = Vector3.zero;
        backAllyPos = frontAllyPos + new Vector3(5, 0, 0);
        frontEnemyPos = backAllyPos + new Vector3(0, 0, 10);
        backEnemyPos = frontEnemyPos + new Vector3(5, 0, 0);
        energizer = new EnergyHandler(3);
        hand = new List<Card>();
        discardDeck = new List<Card>();
        handler = new HandHandler(this);
        executioner = new CardExecutioner(this);
    }

    public void SpawnAllys()
    {
        frontAlly.Spawn(frontAllyPos, combatInventory.Ingemones[0].phenotype);
        backAlly.Spawn(backAllyPos, combatInventory.Ingemones[1].phenotype);
    }

    public void SpawnEnemies()
    {
        frontEnemy.Spawn(frontEnemyPos, "");
        backEnemy.Spawn(backEnemyPos, "");
    }

    public void PlayCard(Card card)
    {
        if (energizer.IsPlayable(card))
        {
            energizer.SpendEnergy(card.info.cost);
            card.info.PlayCard();
            CombatSingletonManager.Instance.eventManager.DiscardCard(card);
        }
        else
            Debug.Log("No enough energy");
    }





    
}