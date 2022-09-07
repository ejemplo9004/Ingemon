using UnityEngine;

public class FailedState : EndBattleState
{
    public override void EnterState(TurnStateManager manager)
    {
        base.EnterState(manager);
        Debug.Log("Se acabó todirijillo");
    }
}