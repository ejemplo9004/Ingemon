using System.Collections;
using System.Collections.Generic;
using Cards;
using UnityEngine;

public class Card
{
    public ScriptableCard cardInfo;
    public int id;

    public Card(int id, ScriptableCard cardInfo)
    {
        this.id = id;
        this.cardInfo = cardInfo;
    }

}
