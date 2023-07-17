using System.Collections.Generic;
using Cards;
using UnityEngine;
using UnityEngine.UI;

public class IngemonCardPlacer : MonoBehaviour
{
    [SerializeField] private GameObject imagePrefab;
    [SerializeField] private GameObject content;
    [SerializeField] private ChosenCardsPanel chosenCardsPanel;

    private Ingemonster ingemon;
    public void PlaceIngemonCards(Dictionary<ScriptableCard, int> ingemonCards, bool reload)
    {
        if (reload)
        {
            var childCount = content.transform.childCount;
            for (int i = childCount - 1; i >= 0; i--)
            {
                var child = content.transform.GetChild(i);
                Destroy(child.gameObject);
            }
        }
        foreach (var (card, count) in ingemonCards)
        {
            for (int i = 0; i < count; i++)
            {
                InstantiateCard(card);
            }
        }
    }

    private void InstantiateCard(ScriptableCard card)
    {
        var imageObject = Instantiate(imagePrefab, content.transform);
        var image = imageObject.GetComponent<Image>();
        var button = imageObject.GetComponent<Button>();
        image.sprite = card.sprite;
        image.preserveAspect = true;
        button.onClick.AddListener(delegate { DeleteCardFromOptions(imageObject, card); });
    }

    private void DeleteCardFromOptions(GameObject cardObject, ScriptableCard card)
    {
        if(chosenCardsPanel.SearchEmptySlot() == -1) return;
        chosenCardsPanel.AddCard(card);
        Destroy(cardObject);
    }

    public void ReturnChosenCardToOptions(ScriptableCard card)
    {
        InstantiateCard(card);
    }

    public void Construct(IngemonDeckManager ingDeckManager, Ingemonster selectedIngemon)
    {
        chosenCardsPanel.Construct(ingDeckManager, selectedIngemon);
    }
}
