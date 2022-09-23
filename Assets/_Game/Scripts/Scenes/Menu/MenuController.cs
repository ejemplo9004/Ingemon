using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private MenuUI menuUI;
    [SerializeField] private Economia economyDB;
    [SerializeField] private PlayerEconomy playerEconomy;

    private void OnEnable() {
        if(RunSingleton.Instance != null){
            DestroyImmediate(RunSingleton.Instance.gameObject);
        }
        ConfigureMenu();
    }
    private void ConfigureMenu(){
        if(GameController.gameController.LastRunPassed){
            menuUI.ShowRewardPanel("Has ganado: " + GameController.gameController.CurrentRun.Reward);
            playerEconomy.AddMoney(GameController.gameController.CurrentRun.Reward);
            economyDB.AddMoney(playerEconomy.money);
        }
    }

    public void SetCurrentRun(Run run)
    {
        GameController.gameController.SetRun(run);
    }
}
