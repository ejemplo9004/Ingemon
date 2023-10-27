using System.Collections.Generic;
using Cards;

[System.Serializable]
public class Ingemonster
{
    #region IngemonInfo
    public string name;
    public string id;
    public string phenotype;
    public int maxHealth;
    public int user_id;
    public int id_carta1;
    public int id_carta2;
    public int id_carta3;
    public int id_carta4;
    public int id_carta5;
    public int id_carta6;

    #endregion

    #region Deck
    public List<ScriptableCard> ingemonColection;
    public List<ScriptableCard> deck;
    //Como serializar las cartas, aunque no tiene sentido serializarlas tampoco.
    #endregion

    public bool VerifyExistence()
    {
        if (name == "") return false;
        if (id == "") return false;
        if (phenotype == "") return false;
        return true;
    }

    public void FillDeck(ScriptableCard card)
    {
        if(deck.Count >= 6) return;
        deck.Add(card);
    }

    public Ingemonster(string id, string name, string phenotype)
    {
        this.id = id;
        this.name = name;
        this.phenotype = phenotype;
        deck = new List<ScriptableCard>();
    }
    public Ingemonster(string id)
    {
        this.id = id;
        deck = new List<ScriptableCard>();
    }

    public Ingemonster(Ingemonster ingemon){
        this.name = ingemon.name;
        this.id = ingemon.id;
        this.phenotype = ingemon.phenotype;
        this.maxHealth = ingemon.maxHealth;
        this.ingemonColection = ingemon.ingemonColection;
        this.deck = ingemon.deck;
    }

    public Ingemonster() {
        deck = new List<ScriptableCard>();
    }
}
