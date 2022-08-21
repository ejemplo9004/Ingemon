using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private MenuUI menuUI;
    [SerializeField] private GameController gameController;

    private void OnEnable() {
        ConfigureMenu();
    }

    private void ConfigureMenu(){
        if(gameController.LastRunPassed){
            menuUI.ShowRewardPanel();
        }
    }
}
