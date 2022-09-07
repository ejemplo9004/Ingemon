using UnityEngine;

public class WinState : EndBattleState
{
    public override void EnterState(TurnStateManager manager)
    {
        base.EnterState(manager);
        Debug.Log("Ganaste prro, celebralo curramba");
    }
}