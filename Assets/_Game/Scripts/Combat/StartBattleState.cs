using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBattleState : TurnState
{
    public override void EnterState(TurnStateManager manager)
    {
        int room = RunSingleton.Instance.run.currentRoomNumber;
        manager.StartCoroutine(SpawnIngemons(manager, room));
    }

    public override void ExitState(TurnStateManager manager)
    {
        Debug.Log("Saliendo del start");
    }

    public override void UpdateState(TurnStateManager manager)
    {
    }

    public void FixDeck(TurnStateManager manager)
    {
        (manager.info.drawDeck, manager.info.enemyDeck) =
            CombatSingletonManager.Instance.cardManager.Init(
                manager.info.frontAlly, 
                manager.info.backAlly, 
                manager.info.frontEnemy, 
                manager.info.backEnemy);
        manager.info.drawDeck = manager.info.handler.ShuffleDeck(manager.info.drawDeck);
        manager.info.enemyDeck = manager.info.handler.ShuffleDeck(manager.info.enemyDeck);
    }

    private IEnumerator SpawnIngemons(TurnStateManager manager, int room)
    {
        Debug.Log("Spawming Ingemons");
        yield return new WaitForSeconds(2);
        manager.info.SpawnAllys(room);
        //yield return new WaitForSeconds(2);
        Debug.Log("Spawn Enemies");
        manager.info.SpawnEnemies(room);
        //yield return new WaitForSeconds(2);
        Debug.Log("BuffsTime");
        CombatSingletonManager.Instance.uiManager.SetHealthBars();
        CombatSingletonManager.Instance.uiManager.UpdateHealthBars();
        FixDeck(manager);
        manager.ChangeState(manager.allyState);
        yield return null;
    }
}