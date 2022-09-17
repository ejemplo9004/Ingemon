using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private MenuUI menuUI;

    private void OnEnable() {
        if(RunSingleton.Instance != null){
            DestroyImmediate(RunSingleton.Instance.gameObject);
        }
        ConfigureMenu();
    }
    private void Start() {
        
    }

    private void ConfigureMenu(){
        if(GameController.gameController.LastRunPassed){
            menuUI.ShowRewardPanel();
        }
    }

    public void SetCurrentRun(Run run)
    {
        GameController.gameController.SetRun(run);
    }
}
