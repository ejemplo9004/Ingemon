using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomUI : GameplaySceneUI
{
    [SerializeField] private GameObject enemyPanel;
    [SerializeField] private List<GameObject> buttons;

    public void FillIngemonsImages(Inventory inventory){
        InstantiateImages(inventory);

        for (int i = 0; i < inventory.Ingemones.Count; i++){
            ingemonsImages[i].GetComponent<IngemonSelector>().Ingemon = inventory.Ingemones[i];
            ingemonsImages[i].GetComponentInChildren<Text>().text = inventory.Ingemones[i].GetComponent<Ingemon>().ingemonName;
        }
    }

    public void InstantiateImages(Inventory runInventory){
        for (int i = 0; i < runInventory.Ingemones.Count; i++)
        {
            GameObject instance = Instantiate(imagePrefab, parent: imagePrefabParent.transform);
            ingemonsImages.Add(instance);
            Button button = instance.GetComponent<Button>();
            button.onClick.AddListener(delegate { SelectIngemon(instance); });
        }
    }

    public void ShowEnemyPanel(){
        enemyPanel.SetActive(true);
    }

    public void ShowButtons(){
        foreach(GameObject button in buttons){
            button.SetActive(true);
        }
    }
}
