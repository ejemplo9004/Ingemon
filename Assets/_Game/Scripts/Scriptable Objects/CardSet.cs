using System.Collections.Generic;
using Cards;
using UnityEngine;

[CreateAssetMenu(fileName = "CardSet", menuName = "Ingemon/CardSet")]
public class CardSet : ScriptableObject
{
    [SerializeField] private List<ScriptableCard> cards;

    public List<ScriptableCard> Get()
    {
        return cards;
    }
}