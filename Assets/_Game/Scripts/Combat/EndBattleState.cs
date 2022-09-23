using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBattleState : TurnState
{
    public override void EnterState(TurnStateManager manager)
    {
        Debug.Log("Fin del combate");
    }

    public override void ExitState(TurnStateManager manager)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState(TurnStateManager manager)
    {
        
    }
    
}
