using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngemonSelector : MonoBehaviour
{
    [SerializeField] private Ingemonster ingemon;
    [SerializeField] private Button selectButton;
    [SerializeField] private IngemonDeckManager deckManager;
    [SerializeField] private GameObject deckPanel;
    private List<GameObject> panelList = new List<GameObject>() { null, null, null, null };

    public bool cardEditionActive;

    private void Start()
    {
        if (cardEditionActive)
        {
            selectButton.onClick.AddListener(GetIngemonCards);
        }
    }

    private void GetIngemonCards()
    {
        string[] feat = ingemon.phenotype.Split("-");
        int race = Int32.Parse(feat[6]);
        switch (race)
        {
            case 0:
                ShowCardPanel(IngemonRace.GENU, 0);
                break;
            case 1:
                ShowCardPanel(IngemonRace.GECU, 1);
                break;
            case 2:
                ShowCardPanel(IngemonRace.GETRE, 2);
                break;
            case 3:
                ShowCardPanel(IngemonRace.GEDO, 3);
                break;
        }
    }

    private void ShowCardPanel(IngemonRace race, int panelIndex)
    {
        var cardDictionary = deckManager.GetUserCardsByRace(race);
        if (panelList[panelIndex] != null)
        {
            panelList[panelIndex].SetActive(true);
            var cardPlacer = panelList[panelIndex].GetComponentInChildren<IngemonCardPlacer>();
            cardPlacer.Construct(deckManager, ingemon);
            cardPlacer.PlaceIngemonCards(cardDictionary, true);
        }
        else
        {
            var deckP = Instantiate(deckPanel);
            panelList[panelIndex] = deckP;
            var cardPlacer = deckP.GetComponentInChildren<IngemonCardPlacer>();
            cardPlacer.Construct(deckManager, ingemon);
            cardPlacer.PlaceIngemonCards(cardDictionary, false);
        }
    }

    public Ingemonster Ingemon { get => ingemon; set => ingemon = value; }
}
