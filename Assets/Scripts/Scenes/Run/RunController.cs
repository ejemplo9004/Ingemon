using System.Collections.Generic;
using UnityEngine;

public class RunController : GameplayScene
{
    [SerializeField] private RunUI runUI;
    [SerializeField] private Inventory inventory;
    private Run run;

    private void OnEnable() {
        run = gameController.CurrentRun;
        ConfigureRun();
    }

    private void ConfigureRun(){
        if(run.runCompleted){
            gameController.LastRunPassed = true;
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
                runUI.ShowRewardPanel();
            }
        }   
        runUI.RoomButtons.ToggleButton(run.currentRoomNumber, true);
    }

    public void AddIngemonToRunInventory(){
        if(!VerifyRunInventory()){
            return;
        }
        foreach (GameObject ingemon in ingemonesSelected)
        {
            runInventory.AddIngemon(ingemon);
        }
    }

    public bool VerifyRunInventory(){
        for (int i = 0; i < ingemonesSelected.Count; i++)
        {
            if(ingemonesSelected[i] == null){
                Debug.Log("No seleccionó los ingemones suficientes");
                return false;
            } 
        }
        runUI.HideSelectionPanel();
        runUI.ShowRoomPanel();
        return true;
    }

}
