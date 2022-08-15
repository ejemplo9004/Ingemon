using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBattleState : TurnState
{
    public override void EnterState(TurnStateManager manager)
    {
        manager.StartCoroutine(SpawnIngemons(manager));
    }

    public override void ExitState(TurnStateManager manager)
    {
        Debug.Log("Saliendo del start");
    }

    public override void UpdateState(TurnStateManager manager)
    {
        
    }

    private IEnumerator SpawnIngemons(TurnStateManager manager)
    {
        Debug.Log("Spawming Ingemons");
        //yield return new WaitForSeconds(2);
        manager.info.SpawnAllys();
        //yield return new WaitForSeconds(2);
        Debug.Log("Spawn Enemies");
        manager.info.SpawnEnemies();
        //yield return new WaitForSeconds(2);
        Debug.Log("BuffsTime");
        manager.ChangeState(manager.allyState);
        yield return null;
    }
}
