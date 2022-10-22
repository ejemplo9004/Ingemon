using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : GameplayScene
{
    [SerializeField] private Inventory combatInventory;
    [SerializeField] private RoomUI gamePlaySceneUI;
    [SerializeField] private GameObject combatController;
    [SerializeField] private RoomTransition roomTransition;
    private List<CombatEventSystem.OnEndBattleAction> failActions = new List<CombatEventSystem.OnEndBattleAction>();
    private void OnEnable()
    {
        ConfigureRoom(true); 
    }
    private void OnDisable()
    {
        if (CombatSingletonManager.Instance != null)
        {
            SetWinListeners(false);
            SetFailListeners(false);
        }
    }
    private void SetWinListeners(bool enable)
    {
        if (enable)
        {
            CombatSingletonManager.Instance.eventManager.OnWinBattle += DisableCombatController;
            return;
        }
        CombatSingletonManager.Instance.eventManager.OnWinBattle -= DisableCombatController;
    }
    private void SetFailListeners(bool enable)
    {
        if (enable)
        {
            failActions.Add(DisableCombatController);
            failActions.Add(delegate { ConfigureRoom(true); });
            foreach (CombatEventSystem.OnEndBattleAction action in failActions)
            {
                CombatSingletonManager.Instance.eventManager.OnFailBattle += action;
            }
            return;
        }
        foreach (CombatEventSystem.OnEndBattleAction action in failActions)
        {
            CombatSingletonManager.Instance.eventManager.OnFailBattle -= action;
        }
        failActions.Clear();
    }
    private IEnumerator SetRoomListeners()
    {
        yield return new WaitUntil(() => CombatSingletonManager.Instance != null);
        Debug.Log("CombatSingletonManager.Instance");
        SetWinListeners(true);
        SetFailListeners(true);
        gamePlaySceneUI.SetWinListeners(true);
        gamePlaySceneUI.SetFailListeners(true);
        CombatSingletonManager.Instance.turnManager.StartBattle();
    }
    public void ConfigureRoom(bool firstConfigure){
        combatInventory.ClearInventory();
        gamePlaySceneUI.ShowSelectionCanvas(true);
        if(firstConfigure) gamePlaySceneUI.FillIngemonsImages(RunInventory);
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
        combatController.SetActive(true);
        gamePlaySceneUI.ShowCombatCanvas(true);
        StartCoroutine(SetRoomListeners());
    }
    public bool VerifyCombatInventory(){
        for (int i = 0; i < ingemonesSelected.Count; i++)
        {
            if(!ingemonesSelected[i].VerifyExistence()){
                Debug.Log("No seleccionó los ingemones suficientes");
                return false;
            } 
        }
        gamePlaySceneUI.HideSelectionPanel();
        gamePlaySceneUI.ShowSelectionCanvas(false);     
        return true;
    }
    private void DisableCombatController()
    {
        combatController.SetActive(false);
    }
    public void AddReward(GameObject reward){
        var rewardable = reward.GetComponent<IReward>();
        rewardable.AddReward();
        gamePlaySceneUI.ShowRewardPanel(false);
        roomTransition.StartRoomTransition();
    }

    public void ExitRun(){
        SceneChanger.ChangeScene(Scenes.MENU);
    }
}