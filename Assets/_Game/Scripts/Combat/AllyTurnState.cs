using System.Collections;
using System.Collections.Generic;
using Cards;
using UnityEngine;

public class AllyTurnState : TurnState
{
    private TurnStateManager manager;
    public override void EnterState(TurnStateManager manager)
    {
        this.manager = manager;
        CombatSingletonManager.Instance.eventManager.OnEndClicked += EndAllyTurn;
        CombatSingletonManager.Instance.eventManager.OnCardPlayed += PlayCard;
        manager.info.energizer.ResetEnergy();
        manager.info.handler.Draw(5);
        manager.info.enemies.PrepareTurn();
        manager.info.frontEnemy.TickStates(BuffTimings.AllyStartTurn);
        manager.info.backEnemy.TickStates(BuffTimings.AllyStartTurn);
        manager.info.backAlly.TickStates(BuffTimings.AllyStartTurn);
        manager.info.frontAlly.TickStates(BuffTimings.AllyStartTurn);
    }

    public override void ExitState(TurnStateManager manager)
    {
        Debug.Log("Saliendo del Ally");
        manager.info.handler.DiscardHand();
        CombatSingletonManager.Instance.eventManager.OnEndClicked -= EndAllyTurn;
        CombatSingletonManager.Instance.eventManager.OnCardPlayed -= PlayCard;
    }

    public override void UpdateState(TurnStateManager manager)
    {
        
    }

    private void EndAllyTurn()
    {
        EndOfTurnEffects(manager);
        if (manager.currentState == this)
            manager.ChangeState(manager.enemyState);
    }

    private void EndOfTurnEffects(TurnStateManager manager)
    {
        manager.info.frontEnemy.TickStates(BuffTimings.AllyEndTurn);
        manager.info.backEnemy.TickStates(BuffTimings.AllyEndTurn);
        manager.info.backAlly.TickStates(BuffTimings.AllyEndTurn);
        manager.info.frontAlly.TickStates(BuffTimings.AllyEndTurn);
        manager.info.frontEnemy.EndTurnClearProtection();
        manager.info.backEnemy.EndTurnClearProtection();
        manager.info.backAlly.HealBleedTick();
        manager.info.frontAlly.HealBleedTick();
        manager.info.backAlly.TickPoison();
        manager.info.frontAlly.TickPoison();
    }

    private void PlayCard(Card card)
    {
        manager.info.PlayCard(card);
    }
}
