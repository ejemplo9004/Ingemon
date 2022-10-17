using System;
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
        description.text = card.info.cardDescription;
        typeText.text = card.info.type.ToString();
    }
}