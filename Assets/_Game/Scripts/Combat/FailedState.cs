using UnityEngine;

public class FailedState : EndBattleState
{
    public override void EnterState(TurnStateManager manager)
    {
        base.EnterState(manager);
        DestroyEntities(manager);
        CheckRunState();
        Debug.Log("Se acabó todirijillo");
    }
    
    private void DestroyEntities(TurnStateManager manager)
    {
        Debug.Log("Borrando ingemones del campo");
        manager.info.DestroyAllys();
        manager.info.DestroyEnemies();
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