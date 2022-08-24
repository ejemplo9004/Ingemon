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
        manager.ChangeState(manager.enemyState);
    }

    private void PlayCard(Card card)
    {
        manager.info.PlayCard(card);
    }
}
