using UnityEngine;

public class FailedState : EndBattleState
{
    public override void EnterState(TurnStateManager manager)
    {
        base.EnterState(manager);
        CheckRunState();
        Debug.Log("Se acabó todirijillo");
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