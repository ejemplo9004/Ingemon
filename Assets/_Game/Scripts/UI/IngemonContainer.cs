using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IngemonContainer : MonoBehaviour
{
    [SerializeField] private GameObject ingemonDetail;
    private GameObject ingemonScrollRect;

    private void Start()
    {
        Spawn(0, 6);
    }

    public void Spawn(int start, int end)
    {
        List<RawImage> rawImages = new List<RawImage>();
        for (int i = start; i < end; i++)
        {
            GameObject copy = Instantiate(ingemonDetail, gameObject.GetComponent<RectTransform>());
            rawImages.Add(copy.GetComponentInChildren<RawImage>());
        }
        IngemonScrollRect scrollrect = ingemonScrollRect.GetComponent<IngemonScrollRect>();
        if (scrollrect != null)
        {
            scrollrect.FillImages(rawImages);
        }
    }
    
    public GameObject IngemonScrollRect1
    {
        get => ingemonScrollRect;
        set => ingemonScrollRect = value;
    }
}
