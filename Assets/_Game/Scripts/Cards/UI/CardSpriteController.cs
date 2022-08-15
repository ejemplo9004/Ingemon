using System;
using Cards;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardSpriteController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI cost;
    public ScriptableCard card { get; private set;  }
    private Button btn;

    public void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(PlayCard);
    }

    public void PlayCard()
    {
        CombatEventSystem.Instance.PlayCard(card);
    }

    public CardSpriteController Titled(string title)
    {
        this.title.SetText(title);
        return this;
    }

    public CardSpriteController WithDescription(string description)
    {
        this.description.SetText(description);
        return this;
    }
    
    public CardSpriteController WithCost(int cost)
    {
        this.cost.SetText(cost.ToString());
        return this;
    }

    public CardSpriteController Card(ScriptableCard card)
    {
        this.card = card;
        return this;
    }

    private void OnDestroy()
    {
        btn.onClick.RemoveListener(PlayCard);
    }
}
