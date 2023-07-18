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
        DestroyEntities(manager);
    }

    public override void ExitState(TurnStateManager manager)
    {
        
    }

    public override void UpdateState(TurnStateManager manager)
    {
        
    }
    
    private void DestroyEntities(TurnStateManager manager)
    {
        Debug.Log("Borrando ingemones del campo");
        manager.info.frontAlly.CleanBuffs();
        manager.info.backAlly.CleanBuffs();
        manager.info.frontEnemy.CleanBuffs();
        manager.info.backEnemy.CleanBuffs();
        manager.info.DestroyAllys();
        manager.info.DestroyEnemies();
    }
    
}
