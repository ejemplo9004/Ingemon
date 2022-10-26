using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CardInfoController : MonoBehaviour
{
    [SerializeField] private CardSpriteController cardSprite;
    [SerializeField] private DescriptionController description;
    public void OnEnable()
    {
        LeanTween.scale(gameObject, Vector3.zero, .13f).setEasePunch();
    }
    public void SetInfo(Card card)
    {
        cardSprite.InitCardSprite(card);
        description.SetDescription(card);
    }
}