using System.Collections.Generic;
using System.Linq;
using Cards;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChosenCardsPanel : MonoBehaviour
{
    [SerializeField] private List<GameObject> cardSlots;
    [SerializeField] private TextMeshProUGUI txtCardSlot;
    
    [SerializeField] private IngemonCardPlacer cardPlacer;

    private List<ScriptableCard> chosenCards = new List<ScriptableCard>(6){null, null, null, null, null, null};
    
    private IngemonDeckManager deckManager;
    private Ingemonster ingemon;
    
    
    public void AddCard(ScriptableCard card)
    {
        var slot = SearchEmptySlot();
        if(slot == -1) return;
        
        chosenCards[slot] = card;
        var index = chosenCards.IndexOf(card);
        
        //cardSlots[index].GetComponent<Image>().sprite = card.sprite;
        //cardSlots[index].GetComponent<Button>().onClick.AddListener(delegate { DeleteCard(index); });
    }

    public int SearchEmptySlot()
    {
        for (int i = 0; i < cardSlots.Count; i++)
        {
            if (chosenCards[i] == null) return i;
        }

        return -1;
    }

    private void DeleteCard(int index)
    {
        if(chosenCards[index] == null) return;
        cardPlacer.ReturnChosenCardToOptions(chosenCards[index]);

        chosenCards[index] = null;
        CleanSlot(index);
    }

    private void CleanSlot(int index)
    {
        cardSlots[index].GetComponent<Image>().sprite = null;
        cardSlots[index].GetComponent<Button>().onClick.RemoveAllListeners();
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
        ingemon = selectedIngemon;
        FillIngemonCards();
        if(deckManager != null) return;
        deckManager = ingDeckManager;
    }

    public void SaveCards()
    {
        if (chosenCards.All(card => card == null))
        {
            deckManager.ClearIngemonDeck(ingemon);
        }
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
        if(ingemon.deck.Count == 0 || ingemon.deck[0] == null) return;
        foreach (var card in ingemon.deck)
        {
            AddCard(card);
        }
    }
    
}
