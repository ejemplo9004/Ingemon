using UnityEngine;

public class WinState : EndBattleState
{
    public override void EnterState(TurnStateManager manager)
    {
        base.EnterState(manager);
        PrepareNextFight();
    }

    private void PrepareNextFight()
    {
        CheckRunState();
        Debug.Log("Ganaste prro, celebralo curramba");
        GameController.gameController.CurrentRun.UnlockNextRoom();
        if (GameController.gameController.CurrentRun.runCompleted)
        {
            Debug.Log("Ganaste la run bro bro");
            GameController.gameController.LastRunPassed = true;
            SceneChanger.ChangeScene(Scenes.MENU);
        }
    }
    private void CheckRunState()
    {
        if (!RunSingleton.Instance.enoughIngemones)
        {
            Debug.Log("Perdiste la run bro :c");
            GameController.gameController.LastRunPassed = false;
            SceneChanger.ChangeScene(Scenes.MENU);
        }
    }
}