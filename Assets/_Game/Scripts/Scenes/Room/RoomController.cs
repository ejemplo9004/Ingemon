using System.Collections.Generic;
using UnityEngine;

public class RoomController : GameplayScene
{
    [SerializeField] private Inventory combatInventory;
    [SerializeField] private RoomUI gamePlaySceneUI;
    private Room room;

    private void OnEnable() {
        room = gameController.CurrentRun.currentRoom;
        ConfigureRoom(); 
    }

    private void ConfigureRoom(){
        combatInventory.ClearInventory();
        gamePlaySceneUI.ChangeBackGroundImage(room.Background);
        gamePlaySceneUI.FillIngemonsImages(runInventory);
        gamePlaySceneUI.ShowEnemyPanel();
    }

    public void AddIngemonToCombatInventory(){
        if(!VerifyCombatInventory()){
            return;
        }
        foreach (Ingemonster ingemon in ingemonesSelected)
        {
            combatInventory.AddIngemon(ingemon);
        }
    }

    public bool VerifyCombatInventory(){
        for (int i = 0; i < ingemonesSelected.Count; i++)
        {
            if(ingemonesSelected[i] == null){
                Debug.Log("No seleccionó los ingemones suficientes");
                return false;
            } 
        }
        gamePlaySceneUI.HideSelectionPanel();
        gamePlaySceneUI.ShowSelectionCanvas(false);
        gamePlaySceneUI.ShowCombatCanvas(true);
        CombatSingletonManager.Instance.turnManager.StartBattle();
        return true;
    }
}
