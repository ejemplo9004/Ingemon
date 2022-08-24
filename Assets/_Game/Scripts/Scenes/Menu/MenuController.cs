using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private MenuUI menuUI;

    private void OnEnable() {
        ConfigureMenu();
    }
    private void Start() {
        
    }

    private void ConfigureMenu(){
        if(GameController.gameController.LastRunPassed){
            menuUI.ShowRewardPanel();
        }
    }
}
