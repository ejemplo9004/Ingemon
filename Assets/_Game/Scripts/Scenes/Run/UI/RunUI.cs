using UnityEngine;
using UnityEngine.UI;
using Cards;
using TMPro;

public class RunUI : GameplaySceneUI
{
    public void FillIngemonsImages(Inventory inventory){
        InstantiateImages(inventory);
        // Pendiente
        for (int i = 0; i < inventory.Ingemones.Count; i++){
            ingemonsImages[i].GetComponent<IngemonSelector>().Ingemon = inventory.Ingemones[i];
            ingemonsImages[i].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Ingemones[i].name;
        }
    }

    public void InstantiateImages(Inventory inventory){
        for (int i = 0; i < inventory.Ingemones.Count; i++)
        {
            GameObject instance = Instantiate(imagePrefab, parent: imagePrefabParent.transform);
            ingemonsImages.Add(instance);
            Button button = instance.GetComponent<Button>();
            button.onClick.AddListener(delegate { SelectIngemon(instance); });
        }
    }
}
