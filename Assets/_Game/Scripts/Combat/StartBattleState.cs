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
        manager.ChangeState(manager.allyState);
        yield return null;
    }
}
