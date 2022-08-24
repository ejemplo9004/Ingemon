using System.Collections;
using System.Collections.Generic;
using Cards;
using UnityEngine;

[System.Serializable]
public class Ingemonster
{
    #region IngemonInfo
    public string name;
    public string id;
    public string phenotype;
    public int maxHealth;

    #endregion

    #region Deck
    public List<ScriptableCard> ingemonColection;
    public List<ScriptableCard> deck;
    //Como serializar las cartas, aunque no tiene sentido serializarlas tampoco.
    #endregion

    public Ingemonster(string id, string name, string phenotype)
    {
        this.id = id;
        this.name = name;
        this.phenotype = phenotype;
    }

    public Ingemonster() { }
}
