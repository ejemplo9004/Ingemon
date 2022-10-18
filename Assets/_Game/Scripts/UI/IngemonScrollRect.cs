using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngemonScrollRect : MonoBehaviour
{
    [SerializeField] private GameObject content;
    [SerializeField] private RectTransform container;
    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private Inventory ingemonInventory;
    [SerializeField] private RenderCreator renderCreator;
    private List<IngemonContainer> ingemonContainers;
    private int gridNumber;
    private float currentValue;
    private float delta;
    private int rawImageCounter;

    public float transitionTime;

    private void Start()
    {
        ingemonContainers = new List<IngemonContainer>();
        gridNumber = Mathf.CeilToInt(ingemonInventory.Ingemones.Count / 6f);
        delta = gridNumber > 1 ? 1f / (gridNumber - 1) : 0;
        SetContainerSize();
        InstantiateGrid();
    }

    private void SetContainerSize()
    {
        float elementsWidht = content.GetComponent<RectTransform>().rect.width;
        container.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, gridNumber * elementsWidht);
    }

    private void InstantiateGrid()
    {
        for (int i = 0; i < gridNumber; i++)
        {
            GameObject copy = Instantiate(content, container);
            copy.GetComponent<IngemonContainer>().IngemonScrollRect1 = gameObject;
            ingemonContainers.Add(copy.GetComponent<IngemonContainer>());
        }
    }

    public void FillImages(List<RawImage> rawImages)
    {
        rawImageCounter++;
        renderCreator.AddImages(rawImages);
        if (rawImageCounter == ingemonContainers.Count)
        {
            renderCreator.gameObject.SetActive(true);
        }
    }

    public void FixScrolling(bool right)
    {
        if (right && currentValue + delta <= 1)
        {
            StartCoroutine(MakeTransition(true, currentValue + delta));
        }
        else if(!right && currentValue - delta >= 0)
        {
            StartCoroutine(MakeTransition(false, currentValue - delta));
        }

    }

    private IEnumerator MakeTransition(bool right, float nextValue)
    {
        float changeFraction = transitionTime * Time.deltaTime;
        if (right)
        {
            for (float i = currentValue; i < nextValue; i += changeFraction)
            {
                scrollbar.value = i;
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            for (float i = currentValue; i > nextValue; i -= changeFraction)
            {
                scrollbar.value = i;
                yield return new WaitForEndOfFrame();
            }
        }
        scrollbar.value = nextValue;
        currentValue = nextValue;
        yield return new WaitForEndOfFrame();

    }
}
