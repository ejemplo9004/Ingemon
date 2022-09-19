using System.Collections;
using System.Collections.Generic;
using Cards;
using UnityEngine;

public class RunDeck
{
    private List<ScriptableCard> deck;

    public void AddCard(ScriptableCard card){
        Deck.Add(card);
    }

    public RunDeck(List<ScriptableCard> runDeck)
    {
        this.Deck = runDeck;
    }

    public List<ScriptableCard> Deck { get => deck; set => deck = value; }

}
