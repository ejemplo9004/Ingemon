using UnityEngine;
using UnityEngine.UI;

public class RunUI : GameplaySceneUI
{
    [SerializeField] private GameObject roomPanel;
    [SerializeField] private GameObject rewardPanel;
    [SerializeField] private RoomButtonsController roomButtons;

    /// <summary>
    /// Rellena la pantalla con las imagenes necesarias para cada Ingemon del jugador, la idea es implementar el carrusel.
    /// </summary>

    public void FillIngemonsImages(Inventory inventory){
        InstantiateImages(inventory);
        // Pendiente
        for (int i = 0; i < inventory.Ingemones.Count; i++){
            ingemonsImages[i].GetComponent<IngemonSelector>().Ingemon = inventory.Ingemones[i];
            ingemonsImages[i].GetComponentInChildren<Text>().text = inventory.Ingemones[i].GetComponent<Ingemon>().ingemonName;
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

    public void ShowRoomPanel(){
        roomPanel.SetActive(true);
    }

    public void ShowRewardPanel(){
        rewardPanel.SetActive(true);
    }

    public RoomButtonsController RoomButtons { get => roomButtons; set => roomButtons = value; }
    
}
