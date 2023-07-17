using UnityEngine;
using TMPro;
public class IngemonDetails : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private GameObject ingemonName;
    [SerializeField] private IngemonSelector ingemonSelector;
    public void CreateInfo(int index)
    {
        Ingemonster ingemon = inventory.Ingemones[index];
        GameObject copy = Instantiate(ingemonName, GetComponent<RectTransform>());
        copy.GetComponentInChildren<TMP_Text>().text = ingemon.name;
        ingemonSelector.Ingemon = ingemon;
    }
}
