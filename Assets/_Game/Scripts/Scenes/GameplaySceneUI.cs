using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameplaySceneUI : MonoBehaviour
{
    [Header("Decoration")]
    [SerializeField] protected Image sceneBackground;
    [Header("Controllers")]
    [SerializeField] protected GameplayScene sceneController;
    [Header("UI Objects")]
    [SerializeField] protected GameObject ingemonSelectionPanel;
    [SerializeField] protected GameObject imagePrefab;
    [SerializeField] protected GameObject imagePrefabParent;
    [SerializeField] protected GameObject selectedIngemonImage;
    [Header("UI Object Lists")]
    [SerializeField] protected List<GameObject> ingemonsImages;
    [SerializeField] protected List<GameObject> preselectionImages;
    [SerializeField] protected GameObject selectionCanvas;

    public void SelectIngemon(GameObject selectedImage, bool room){
        Ingemonster ingemon = selectedImage.GetComponent<IngemonSelector>().Ingemon;
        int index = preselectionImages.IndexOf(selectedIngemonImage);
        if(sceneController.AddIngemonToPreselection(ingemon, index)){
            selectedIngemonImage.GetComponent<IngemonSelector>().Ingemon = ingemon;
            selectedIngemonImage.GetComponentInChildren<TextMeshProUGUI>().text = ingemon.name;
            
            if (!room)
            {
                selectedIngemonImage.GetComponentInChildren<RawImage>().texture =
                    selectedImage.GetComponentInChildren<RawImage>().texture;
            }
            
            SetNextPreselectionBox();
        }
    }

    public void CleanPreselection(GameObject image){
        sceneController.RemoveIngemonFromPreselection(image.GetComponent<IngemonSelector>().Ingemon);
        image.GetComponent<IngemonSelector>().Ingemon = new Ingemonster();
        image.GetComponentInChildren<TextMeshProUGUI>().text = "";
        SetNextPreselectionBox();
    }

    private void SetNextPreselectionBox(){
        foreach (GameObject image in preselectionImages)
        {
            Ingemonster imageIngemon = image.GetComponent<IngemonSelector>().Ingemon;
            if(imageIngemon.name == "" || imageIngemon.name == null){
                selectedIngemonImage = image;
                return;
            }
        }
    }

    public void ChangeBackGroundImage(Sprite background){
        sceneBackground.sprite = background;
    }

    public void ShowIngemonSelectionPanel(){
        ingemonSelectionPanel.SetActive(true);
    }

    public void HideSelectionPanel(){
        ingemonSelectionPanel.SetActive(false);
    }

    public void SetSelectedIngemonImage(GameObject image){
        selectedIngemonImage = image;
    }

    public void ShowSelectionCanvas(bool show){
        selectionCanvas.SetActive(show);
    }
    public GameObject SelectedIngemonImage { get => selectedIngemonImage; set => selectedIngemonImage = value; }
}
