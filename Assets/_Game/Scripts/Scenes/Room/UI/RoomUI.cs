using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomUI : GameplaySceneUI
{
    [SerializeField] private GameObject enemyPanel;
    [SerializeField] private GameObject combatCanvas;
    [SerializeField] private GameObject rewardPanel;
    private List<CombatEventSystem.OnEndBattleAction> winActions = new List<CombatEventSystem.OnEndBattleAction>();
    private List<CombatEventSystem.OnEndBattleAction> failActions = new List<CombatEventSystem.OnEndBattleAction>();

    private void OnDisable()
    {
        if (CombatSingletonManager.Instance != null)
        {
            SetWinListeners(false);
            SetFailListeners(false);
        }
    }

    public void FillIngemonsImages(Inventory inventory){
        InstantiateImages(inventory);

        for (int i = 0; i < inventory.Ingemones.Count; i++){
            ingemonsImages[i].GetComponent<IngemonSelector>().Ingemon = inventory.Ingemones[i];
            ingemonsImages[i].GetComponentInChildren<Text>().text = inventory.Ingemones[i].name;
        }
    }

    public void SetWinListeners(bool enable)
    {
        if (enable)
        {
            winActions.Add(delegate { ShowRewardPanel(true); });
            winActions.Add(delegate { ShowCombatCanvas(false); });
            foreach (CombatEventSystem.OnEndBattleAction action in winActions)
            {
                CombatSingletonManager.Instance.eventManager.OnWinBattle += action;
            }
            CombatSingletonManager.Instance.eventManager.OnWinBattle += CleanPreselectionImages;
            return;
        }
        foreach (CombatEventSystem.OnEndBattleAction action in winActions)
        {
            CombatSingletonManager.Instance.eventManager.OnWinBattle -= action;
        }
        winActions.Clear();
        CombatSingletonManager.Instance.eventManager.OnWinBattle -= CleanPreselectionImages;
    }

    public void SetFailListeners(bool enable)
    {
        if (enable)
        {
            failActions.Add(delegate { ShowCombatCanvas(false); });
            foreach (CombatEventSystem.OnEndBattleAction action in failActions)
            {
                CombatSingletonManager.Instance.eventManager.OnFailBattle += action;
            }
            CombatSingletonManager.Instance.eventManager.OnFailBattle += CleanPreselectionImages;
            return;
        }
        foreach (CombatEventSystem.OnEndBattleAction action in failActions)
        {
            CombatSingletonManager.Instance.eventManager.OnFailBattle -= action;
        }
        failActions.Clear();
        CombatSingletonManager.Instance.eventManager.OnFailBattle -= CleanPreselectionImages;
    }

    public void InstantiateImages(Inventory runInventory)
    {
        DestroyIngemonImages();
        for (int i = 0; i < runInventory.Ingemones.Count; i++)
        {
            GameObject instance = Instantiate(imagePrefab, parent: imagePrefabParent.transform);
            ingemonsImages.Add(instance);
            Button button = instance.GetComponent<Button>();
            button.onClick.AddListener(delegate { SelectIngemon(instance); });
        }
    }

    private void DestroyIngemonImages()
    {
        foreach (GameObject image in ingemonsImages)
        {
            Destroy(image);
        }
        ingemonsImages.Clear();
    }

    private void CleanPreselectionImages()
    {
        foreach (GameObject preselectionImage in preselectionImages)
        {
            CleanPreselection(preselectionImage);
        }
    }

    public void ShowEnemyPanel(){
        enemyPanel.SetActive(true);
    }
    public void ShowCombatCanvas(bool show){
        combatCanvas.SetActive(show);
    }
    
    public void ShowRewardPanel(bool state){
        if (!GameController.gameController.CurrentRun.runCompleted)
        {
            rewardPanel.SetActive(state);
        }
    }
}
