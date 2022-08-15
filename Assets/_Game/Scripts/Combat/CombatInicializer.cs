﻿using System;
using System.Collections.Generic;
using System.Linq;
using Cards;
using UnityEngine;

public class CombatInicializer : MonoBehaviour
{
    public TurnStateManager manager;
    private IngemonController frontAlly, backAlly;
    private EnemyController frontEnemy, backEnemy;
    public IngemonController baseIngemon;
    public EnemyController baseEnemy;
    public List<ScriptableCard> baseCollection;

    public void Awake()
    {
        ScriptableCard[] cards = Resources.FindObjectsOfTypeAll<ScriptableCard>();
        foreach (var card in cards)
        {
            Debug.Log($"{card.cardName} added!");
            baseCollection.Add(card);
        }
        SetIngemons();
    }

    public void Start()
    {

    }

    private void SetIngemons()
    {
        frontAlly = Instantiate(baseIngemon, Vector3.zero, Quaternion.identity);
        frontAlly.ingemonInfo = new IngemonBuilder().WithName("Karumon")
            .WithMaxHealth(100)
            .WithDeck(baseCollection);
        frontAlly.SetHealth();
        
        backAlly = Instantiate(baseIngemon, Vector3.zero, Quaternion.identity);
        backAlly.ingemonInfo = new IngemonBuilder().WithName("Flagymon")
            .WithMaxHealth(100)
            .WithDeck(baseCollection);
        backAlly.SetHealth();
        
        frontEnemy = Instantiate(baseEnemy, Vector3.zero, Quaternion.identity);
        frontEnemy.ingemonInfo = new IngemonBuilder().WithName("Fishamon")
            .WithMaxHealth(100)
            .WithDeck(baseCollection);
        frontEnemy.SetHealth();

        backEnemy = Instantiate(baseEnemy, Vector3.zero, Quaternion.identity);
        backEnemy.ingemonInfo = new IngemonBuilder().WithName("Corromon")
            .WithMaxHealth(100)
            .WithDeck(baseCollection);
        backEnemy.SetHealth();

        manager.info = new CombatInfo(frontAlly, backAlly, frontEnemy, backEnemy);
        manager.info.drawDeck = CreateDeck();
        manager.info.ShuffleDeck();
    }

    private List<ScriptableCard> CreateDeck() => new List<ScriptableCard>(
        frontAlly.ingemonInfo.deck
            .Concat(backAlly.ingemonInfo.deck));
}