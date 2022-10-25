using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RunUI : GameplaySceneUI
{
    [SerializeField] private RenderCreator renderCreator;
    [SerializeField] private GameObject sizeReference;
    [SerializeField] private Scrollbar scrollbar;
    public float viewDivisorFactor;
    public float slideSpeed;
    private int scrollDirection = 1;
    private bool isScrolling;

    private void Update()
    {
        if (isScrolling)
        {
            SlideScroll();
        }
    }

    public void FillIngemonsImages(Inventory inventory){
        InstantiateImages(inventory);
        for (int i = 0; i < inventory.Ingemones.Count; i++){
            ingemonsImages[i].GetComponent<IngemonSelector>().Ingemon = inventory.Ingemones[i];
        }
    }

    public void InstantiateImages(Inventory inventory)
    {
        List<RawImage> ri = new List<RawImage>();
        for (int i = 0; i < inventory.Ingemones.Count; i++)
        {
            GameObject instance = Instantiate(imagePrefab, parent: imagePrefabParent.transform);
            Rect elementsSize = sizeReference.GetComponent<RectTransform>().rect;
            instance.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, elementsSize.width / viewDivisorFactor);
            instance.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, elementsSize.height);
            ingemonsImages.Add(instance);
            ri.Add(instance.GetComponentInChildren<RawImage>());
            Button button = instance.GetComponent<Button>();
            button.onClick.AddListener(delegate { SelectIngemon(instance, false); });
        }
        renderCreator.AddImages(ri);
    }

    public void ActivateScroll(bool right)
    {
        if (right)
        {
            scrollDirection = 1;
        }
        else
        {
            scrollDirection = -1;
        }

        isScrolling = true;
    }

    public void DisableScroll()
    {
        isScrolling = false;
    }

    private void SlideScroll()
    {
        scrollbar.value += scrollDirection * slideSpeed * Time.deltaTime;
    }
}
