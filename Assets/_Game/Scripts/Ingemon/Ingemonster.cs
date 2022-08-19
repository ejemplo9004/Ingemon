using System.Collections;
using System.Collections.Generic;
using Cards;
using UnityEngine;

public class Ingemonster
{
    #region IngemonInfo
    public string name { get; set; }
    public string id { get; }
    public string phenotype { get; set; }
    public int maxHealth { get; set; }

    #endregion

    #region Deck
    public List<ScriptableCard> ingemonColection;
    public List<ScriptableCard> deck;
    #endregion

    public Ingemonster(string id)
    {
        this.id = id;
    }
}
