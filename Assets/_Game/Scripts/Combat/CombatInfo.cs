using System;
using System.Collections;
using System.Collections.Generic;
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
    public EnemyActions enemies;
    public DeadController deadController;
    
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
        backAllyPos = frontAllyPos + new Vector3(2, 0, -2);
        frontEnemyPos = frontAllyPos + new Vector3(-8, 0, 2);
        backEnemyPos = frontAllyPos + new Vector3(-13, 0, -1);
        energizer = new EnergyHandler(3);
        hand = new List<Card>();
        discardDeck = new List<Card>();
        handler = new HandHandler(this);
        executioner = new CardExecutioner(this);
        enemies = new EnemyActions(this);
        deadController = new DeadController();
        CombatSingletonManager.Instance.eventManager.OnIngemonDead += CheckEnd;
    }

    public void SpawnAllys(int room)
    {
        frontAlly.Spawn(frontAllyPos, combatInventory.Ingemones[0], room);
        frontAlly.BattlePosition(true);
        backAlly.Spawn(backAllyPos, combatInventory.Ingemones[1], room);
        backAlly.BattlePosition(true);
    }

    public void SpawnEnemies(int room)
    {
        frontEnemy.Spawn(frontEnemyPos, frontEnemy.ingemonInfo, room);
        backEnemy.Spawn(backEnemyPos, backEnemy.ingemonInfo, room);
    }

    public void DestroyAllys()
    {
        frontAlly.DestroyIngemon();
        backAlly.DestroyIngemon();
    }

    public void DestroyEnemies()
    {
        frontEnemy.DestroyIngemon();
        backEnemy.DestroyIngemon();
    }

    public void PlayCard(Card card)
    {
        if (energizer.IsPlayable(card))
        {
            energizer.SpendEnergy(card.info.cost);
            card.info.PlayCard(card.owner);
            CombatSingletonManager.Instance.eventManager.ValidCardPlayed(card);
            CombatSingletonManager.Instance.eventManager.DiscardCard(card);
            handler.Discard(card);
            card.owner.TickBleed();
        }
        else
            Debug.Log("No enough energy");
    }

    public void PurgeCardsFromDeckAfterAnIngemonDie(EntityController ingemon)
    {
        drawDeck.RemoveAll(card => card.owner.Equals(ingemon));
        hand.RemoveAll(card => card.owner.Equals(ingemon));
        discardDeck.RemoveAll(card => card.owner.Equals(ingemon));
        CombatSingletonManager.Instance.eventManager.UpdateHand(hand);
    }

    private void CheckEnd(EntityController dead)
    {
        DeleteDeadIngemons();
        if (frontAlly.CheckDead() && backAlly.CheckDead())
        {
            dead.StartCoroutine(CallFail());
        }

        if (frontEnemy.CheckDead() && backEnemy.CheckDead())
        {
            dead.StartCoroutine(CallWin());
        }
    }

    private void DeleteDeadIngemons()
    {
        if (frontAlly.CheckDead())
        {
            RunSingleton.Instance.RunInventory.DeleteIngemon(frontAlly.ingemonInfo.phenotype);
        }

        if (backAlly.CheckDead())
        {
            RunSingleton.Instance.RunInventory.DeleteIngemon(backAlly.ingemonInfo.phenotype);
        }
    }

    public EntityController GetIngemon(CombatIngemonPosition ingemon)
    {
        switch (ingemon)
        {
            case CombatIngemonPosition.FRONT_ALLY:
                return frontAlly;
            case CombatIngemonPosition.BACK_ALLY:
                return backAlly;
            case CombatIngemonPosition.FRONT_ENEMY:
                return frontEnemy;
            case CombatIngemonPosition.BACK_ENEMY:
                return backEnemy;
            default:
                throw new ArgumentOutOfRangeException(nameof(ingemon), ingemon, null);
        }
    }

    private IEnumerator CallWin()
    {
        handler.DiscardHand();
        yield return new WaitForSeconds(2f);
        CombatSingletonManager.Instance.eventManager.WinBattle();
        yield return null;
    }    
    
    private IEnumerator CallFail()
    {
        yield return new WaitForSeconds(2f);
        CombatSingletonManager.Instance.eventManager.FailedBattle();
        yield return null;
    }

    public void HackBattle(int code)
    {
        switch (code)
        {
            case 1:
                CombatSingletonManager.Instance.eventManager.WinBattle();
                break;
            case 2:
                RunSingleton.Instance.DeleteIngemonFromRun(frontAlly.ingemonInfo.phenotype);
                CombatSingletonManager.Instance.eventManager.WinBattle();
                break;
            case 3:
                RunSingleton.Instance.DeleteIngemonFromRun(backAlly.ingemonInfo.phenotype);
                CombatSingletonManager.Instance.eventManager.WinBattle();
                break;
            case 4:
                RunSingleton.Instance.DeleteIngemonFromRun(frontAlly.ingemonInfo.phenotype);
                RunSingleton.Instance.DeleteIngemonFromRun(backAlly.ingemonInfo.phenotype);
                CombatSingletonManager.Instance.eventManager.FailedBattle();
                break;
            default:
                break;
        }
    }
    
    public void WinNow()
    {
        CombatSingletonManager.Instance.eventManager.WinBattle();
    }
}