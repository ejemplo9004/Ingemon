﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DescriptionController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI typeText;
    [SerializeField] private Image typeIcon;

    public void SetDescription(Card card)
    {
        title.text = card.info.cardName;
        description.text = card.info.cardPhrase;
        typeText.text = cardTypeString(card.info.type);
    }
    public string cardTypeString(CardType ct){
        switch (ct){
            case CardType.ATTACK:
                return "Ataque";
            case CardType.DEFENSE:
                return "Defensa";
            case CardType.DEBUFF:
                return "Debuff";
            case CardType.BUFF:
                return "Buff";          
            default:
                return "Carta";
        }
    }
}