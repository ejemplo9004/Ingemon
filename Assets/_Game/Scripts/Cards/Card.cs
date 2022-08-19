using System.Collections;
using System.Collections.Generic;
using Cards;
using UnityEngine;

public class Card
{
    public ScriptableCard info;
    public int id;
    public EntityController owner;

    public Card(int id, ScriptableCard info, EntityController owner)
    {
        this.id = id;
        this.info = info;
        this.owner = owner;
    }

}
