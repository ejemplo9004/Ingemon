using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class IngemonScrollRect : MonoBehaviour
{
    [SerializeField] private GameObject content;
    [SerializeField] private RectTransform container;
    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private Inventory ingemonInventory;
    private List<IngemonContainer> ingemonContainers;
    private int gridNumber;
    private float currentValue;
    private float delta;

    public float transitionTime;

    private void Start()
    {
        ingemonContainers = new List<IngemonContainer>();
        gridNumber = Mathf.CeilToInt(ingemonInventory.Ingemones.Count / 6f);
        delta = gridNumber > 0 ? 1f / (gridNumber - 1) : 0;
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
            ingemonContainers.Add(copy.GetComponent<IngemonContainer>());
        }
    }

    private void FillIngemons()
    {
        for (int i = 0; i < ingemonContainers.Count; i++)
        {
            if (i*6 + 6 < ingemonContainers.Count)
            {
                
            }
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
        if (transitionTime >= 1) transitionTime = 0.99f;
        float changeFraction = delta * transitionTime * Time.deltaTime;
        if (right)
        {
            for (float i = currentValue; i < nextValue; i += changeFraction)
            {
                scrollbar.value = i;
            }

        }
        else
        {
            for (float i = currentValue; i > nextValue; i -= changeFraction)
            {
                scrollbar.value = i;
            }
        }

        currentValue = nextValue;
        yield return new WaitForEndOfFrame();

    }
}
