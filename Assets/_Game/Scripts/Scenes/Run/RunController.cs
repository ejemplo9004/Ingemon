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
        run.RestartValues();
        ConfigureRun();
    }

    private void ConfigureRun(){
        GameController.gameController.LastRunPassed = false;
        runUI.ChangeBackGroundImage(run.Background);
        if(!run.IngemonsWereSelected){
            runUI.FillIngemonsImages(inventory);
            runUI.ShowIngemonSelectionPanel();
            run.IngemonsWereSelected = true;
            run.runCompleted = false;
        }
    }

    public void AddIngemonToRunInventory(){
        if(!VerifyRunInventory()){
            return;
        }
        foreach (Ingemonster ingemon in ingemonesSelected)
        {
            RunInventory.AddIngemon(ingemon);
        }
        SceneChanger.ChangeScene(Scenes.ROOM);
    }

    public bool VerifyRunInventory(){
        for (int i = 0; i < ingemonesSelected.Count; i++)
        {
            if(!ingemonesSelected[i].VerifyExistence()){
                Debug.Log("No seleccionó los ingemones suficientes");
                return false;
            } 
        }
        return true;
    }
}
