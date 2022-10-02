using System.Collections;
using System.Collections.Generic;
using Cards;
using UnityEngine;

public class IngemonBuilder{

    public Ingemonster ingemon;
    public IngemonBuilder()
    {
        // GET INGEMONSTER ID
        ingemon = new Ingemonster();
    }

    public IngemonBuilder WithName(string name)
    {
        ingemon.name = name;
        return this;
    }

    public IngemonBuilder WithMaxHealth(int mh)
    {
        ingemon.maxHealth = mh;
        return this;
    }

    public IngemonBuilder WithPhenotype(string pt)
    {
        ingemon.phenotype = pt;
        return this;
    }

    public IngemonBuilder WithDeck(List<ScriptableCard> cards)
    {
        ingemon.deck = cards;
        return this;
    }

    private Ingemonster BuildIngemon()
    {
        return ingemon;
    }

    public static implicit operator Ingemonster(IngemonBuilder ib)
    {
        return ib.BuildIngemon();
    }
}
