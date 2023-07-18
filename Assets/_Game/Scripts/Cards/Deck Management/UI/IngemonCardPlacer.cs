using System;
using System.Collections.Generic;
using Cards;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IngemonCardPlacer : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private UnselectedCardComponents cardUIComponents;
    [SerializeField] private GameObject content;
    [SerializeField] private ChosenCardsPanel chosenCardsPanel;
    
    [SerializeField] private List<ScriptableCard> chosenCards = new List<ScriptableCard>(6){null, null, null, null, null, null};

    [SerializeField] private List<GameObject> cardSlots = new List<GameObject>(6){null, null, null, null, null, null};

    [SerializeField] private List<GameObject> allCards = new List<GameObject>(6);
    //[SerializeField] private List<GameObject> eligibleCards;
    private IngemonDeckManager deckManager;
    private Ingemonster ingemon;
    public void AddCard(ScriptableCard card)
    {
        var slot = SearchEmptySlot();
        if(slot == -1) return;
        chosenCards[slot] = card;
    }
    public int SearchEmptySlot()
    {
        for (int i = 0; i < chosenCards.Count; i++)
        {
            if (chosenCards[i] == null) return i;
        }
        return -1;
    }
    private void DeleteCard(int index)
    {
        Debug.Log("Delete card:" + index);
        if(chosenCards[index] == null) return;
        //cardPlacer.ReturnChosenCardToOptions(chosenCards[index]);
        CleanSlot(index);
        cardSlots[index] = null;
        chosenCards[index] = null;
    }
    private void CleanSlot(int index)
    {
        cardSlots[index].GetComponent<Image>().color = Color.white;
        cardSlots[index].GetComponent<Button>().onClick.RemoveAllListeners();
        var cleanedSlot = cardSlots[index];
        cleanedSlot.GetComponent<Button>().onClick.AddListener(delegate { DeleteCardFromOptions(cleanedSlot, chosenCards[index]); });
        Debug.Log(cardSlots[index]);
        Debug.Log(chosenCards[index]);
    }
    public void CleanAllSlots()
    {
        for (int i = 0; i < cardSlots.Count; i++)
        {
            CleanSlot(i);
            
        }

        for (int i = 0; i < chosenCards.Count; i++)
        {
            chosenCards[i] = null;
        }
    }
    public void Construct(IngemonDeckManager ingDeckManager, Ingemonster selectedIngemon)
    {
        //chosenCardsPanel.Construct(ingDeckManager, selectedIngemon);
        ingemon = selectedIngemon;
        FillIngemonCards();
        if(deckManager != null) return;
        deckManager = ingDeckManager;
    }
    public void SaveCards()
    {
        if(chosenCards.Contains(null))return;
        List<ScriptableCard> cardsCopy = new List<ScriptableCard>(ingemon.deck);
        foreach (var card in cardsCopy)
        {
            if (!chosenCards.Contains(card))
            {
                deckManager.DeleteCardFromIngemon(ingemon, card);
            }
            
        }
        deckManager.ReturnIngemonCardsToAvailable(ingemon);
        ingemon.deck.Clear();
        foreach (var card in chosenCards)
        {
            deckManager.AddCardToIngemon(ingemon, card);
        }
    }
    private void FillIngemonCards()
    {
        if(ingemon.deck.Count == 0) return;
        foreach (var card in ingemon.deck)
        {
            AddCard(card);
        }
    }
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
        var cardUI = Instantiate(cardPrefab, content.transform);
        var button = cardUI.GetComponent<Button>();
        var cardComponents = cardUI.GetComponent<UnselectedCardComponents>(); 
        
        cardComponents.imgCard.sprite = card.sprite;
        cardComponents.imgCard.preserveAspect = true;
        cardComponents.txtCardTitle.text = card.cardName;
        cardComponents.txtCardDescription.text = card.cardDescription;
        cardComponents.txtCardEnergy.text = card.cost.ToString();
        allCards.Add(cardUI);
        button.onClick.AddListener(delegate { DeleteCardFromOptions(cardUI, card); });
        if (ingemon.deck.Contains(card))
        {
            button.onClick.Invoke();
        }
    }

    private void DeleteCardFromOptions(GameObject cardObject, ScriptableCard card)
    {
        
        int index = SearchEmptySlot();
        if(SearchEmptySlot() == -1) return;
        Debug.Log("'executedddddddddddddddddddddd'");
        Debug.Log(cardObject);
        cardSlots[index] = cardObject;
        AddCard(card);
        cardObject.GetComponent<Image>().color = Color.green;
        cardSlots[index].GetComponent<Button>().onClick.RemoveAllListeners();
        cardSlots[index].GetComponent<Button>().onClick.AddListener(delegate { DeleteCard(index); });
        //AddCard(card);
        //chosenCardsPanel.AddCard(card);
        //Destroy(cardObject);
    }

    public void ReturnChosenCardToOptions(ScriptableCard card)
    {
        InstantiateCard(card);
    }
}
