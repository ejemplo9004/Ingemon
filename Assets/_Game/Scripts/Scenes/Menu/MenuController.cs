using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private MenuUI menuUI;
    [SerializeField] private PlayerMoney playerMoney;
    [SerializeField] private PlayerEconomy playerEconomy;

    private void OnEnable() {
        if(RunSingleton.Instance != null){
            DestroyImmediate(RunSingleton.Instance.gameObject);
        }
        ConfigureMenu();
    }
    private void ConfigureMenu(){
        if(GameController.gameController.LastRunPassed){
            menuUI.ShowRewardPanel("Has ganado: " + GameController.gameController.CurrentRun.Reward + " monedas");
            playerEconomy.AddMoney(GameController.gameController.CurrentRun.Reward);
            StartCoroutine(playerMoney.AddMoneyCoroutine(playerEconomy.money, false));
            //aca setear fecha de juego
        }
    }

    public void SetCurrentRun(Run run)
    {
        //aca revisar si ya jugo
        GameController.gameController.SetRun(run);
        SceneChanger.ChangeScene(2);
    }
}
