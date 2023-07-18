using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnState : TurnState
{
    public override void EnterState(TurnStateManager manager)
    {
        Debug.Log("End Turn bitch");
        manager.StartCoroutine(EnemyTurn(manager));
        manager.info.frontEnemy.TickStates(BuffTimings.EnemyStartTurn);
        manager.info.backEnemy.TickStates(BuffTimings.EnemyStartTurn);
        manager.info.backAlly.TickStates(BuffTimings.EnemyStartTurn);
        manager.info.frontAlly.TickStates(BuffTimings.EnemyStartTurn);
    }

    public override void ExitState(TurnStateManager manager)
    {
        CombatSingletonManager.Instance.eventManager.IntentionsClean();
        Debug.Log("Saliendo de turno enemigo");
    }

    public override void UpdateState(TurnStateManager manager)
    {
        
    }

    private IEnumerator EnemyTurn(TurnStateManager manager)
    {
        int cardsPlayed = manager.info.enemies.PlayTurn();
        yield return new WaitForSeconds(cardsPlayed * 1.5f);
        EndOfTurnEffects(manager);
        if (manager.currentState == this)
            manager.ChangeState(manager.allyState);
    }
    
    private void EndOfTurnEffects(TurnStateManager manager)
    {
        manager.info.frontEnemy.TickStates(BuffTimings.EnemyEndTurn);
        manager.info.backEnemy.TickStates(BuffTimings.EnemyEndTurn);
        manager.info.backAlly.TickStates(BuffTimings.EnemyEndTurn);
        manager.info.frontAlly.TickStates(BuffTimings.EnemyEndTurn);
        manager.info.frontAlly.ClearProtection();
        manager.info.backAlly.ClearProtection();
        manager.info.backEnemy.HealBleedTick();
        manager.info.frontEnemy.HealBleedTick();
        manager.info.backEnemy.TickPoison();
        manager.info.frontEnemy.TickPoison();
        manager.info.deadController.LetIngemonDie();
    }

}
