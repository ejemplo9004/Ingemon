using System.Collections.Generic;
using UnityEngine;
using Cards;

public class RunController : GameplayScene
{
    [SerializeField] private RunUI runUI;
    [SerializeField] private Inventory inventory;
    private Run run;

    private void OnEnable() {
        run = GameController.gameController.CurrentRun;
        ConfigureRun();
    }

    private void ConfigureRun(){
        if(run.runCompleted){
            GameController.gameController.LastRunPassed = true;
            SceneChanger.ChangeScene(0);
        }
        runUI.ChangeBackGroundImage(run.Background);
        if(!run.IngemonsWereSelected){
            runUI.FillIngemonsImages(inventory);
            runUI.ShowIngemonSelectionPanel();
            run.IngemonsWereSelected = true;
            run.runCompleted = false;
        }
        else{
            runUI.ShowRoomPanel();
            if(run.lastFightPassed){
                runUI.ShowRewardPanel(true);
            }
        }   
        runUI.RoomButtons.ToggleButton(run.currentRoomNumber, true);
    }

    public void AddIngemonToRunInventory(){
        if(!VerifyRunInventory()){
            return;
        }
        foreach (Ingemonster ingemon in ingemonesSelected)
        {
            RunInventory.AddIngemon(ingemon);
        }
    }

    public bool VerifyRunInventory(){
        for (int i = 0; i < ingemonesSelected.Count; i++)
        {
            if(!ingemonesSelected[i].VerifyExistence()){
                Debug.Log("No seleccionó los ingemones suficientes");
                return false;
            } 
        }
        runUI.HideSelectionPanel();
        runUI.ShowRoomPanel();
        return true;
    }

    public void AddReward(GameObject reward){
        var rewardable = reward.GetComponent<IReward>();
        rewardable.AddReward();
        runUI.ShowRewardPanel(false);
    }

}
