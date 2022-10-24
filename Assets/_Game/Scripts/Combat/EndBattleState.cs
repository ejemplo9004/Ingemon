using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBattleState : TurnState
{
    public override void EnterState(TurnStateManager manager)
    {
        Debug.Log("Fin del combate");
        manager.info.backAlly.BattlePosition(false);
        manager.info.frontAlly.BattlePosition(false);
    }

    public override void ExitState(TurnStateManager manager)
    {
        
    }

    public override void UpdateState(TurnStateManager manager)
    {
        
    }
    
}
