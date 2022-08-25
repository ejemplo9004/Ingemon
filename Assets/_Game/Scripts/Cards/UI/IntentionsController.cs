using System;
using System.Collections.Generic;
using UnityEngine;

public class IntentionsController: MonoBehaviour
{
    [SerializeField] private GameObject attack, defense, buff, debuff;
    private List<GameObject> intentions;

    public void Start()
    {
        intentions = new List<GameObject>();
    }

    public void SetIntentions(List<Card> cards)
    {
        foreach (var card in cards)
        {
            CardType type = card.info.type;
            GameObject intent;
            switch (type)
            {
                case CardType.ATTACK:
                    intent = attack;
                    break;
                case CardType.DEFENSE:
                    intent = defense;
                    break;
                case CardType.BUFF:
                    intent = buff;
                    break;
                case CardType.DEBUFF:
                    intent = debuff;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            intentions.Add(Instantiate(intent, transform));
        }
    }

    public void CleanIntentions()
    {
        int count = intentions.Count;
        Debug.Log($"Intentions : {count}");
        for (int i = count - 1; i >= 0; i--)
        {
            Destroy(intentions[i].gameObject);
            intentions.RemoveAt(i);
        }
    }
}