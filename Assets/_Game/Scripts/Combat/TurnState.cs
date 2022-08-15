
public abstract class TurnState
{
    public abstract void EnterState(TurnStateManager manager);
    public abstract void UpdateState(TurnStateManager manager);
    public abstract void ExitState(TurnStateManager manager);
}
