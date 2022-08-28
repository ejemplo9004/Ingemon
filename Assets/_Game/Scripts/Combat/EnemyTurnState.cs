using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnState : TurnState
{
    public override void EnterState(TurnStateManager manager)
    {
        Debug.Log("End Turn bitch");
        manager.StartCoroutine(EnemyTurn(manager));
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
        manager.info.enemies.PlayTurn();
        yield return new WaitForSeconds(3);
        manager.ChangeState(manager.allyState);
        yield return null;
    }

    private void CheckEnd(EntityController ingemon)
    {
        
    }

}
