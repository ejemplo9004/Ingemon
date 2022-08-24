using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public void SelectIngemon(GameObject selectedImage){
        Ingemonster ingemon = selectedImage.GetComponent<IngemonSelector>().Ingemon;
        int index = preselectionImages.IndexOf(selectedIngemonImage);
        if(sceneController.AddIngemonToPreselection(ingemon, index)){
            selectedIngemonImage.GetComponent<IngemonSelector>().Ingemon = ingemon;
            selectedIngemonImage.GetComponentInChildren<Text>().text = ingemon.name;
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
        selectedIngemonImage.GetComponentsInChildren<Image>(true)[1].gameObject.SetActive(false);
        selectedIngemonImage = image;
        image.GetComponentsInChildren<Image>(true)[1].gameObject.SetActive(true);
    }

    public void ShowSelectionCanvas(bool show){
        selectionCanvas.SetActive(show);
    }
    public GameObject SelectedIngemonImage { get => selectedIngemonImage; set => selectedIngemonImage = value; }
}
