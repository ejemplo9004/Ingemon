using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardSpriteController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private Image panel;
    public Card card { get; private set;  }
    public int cardId { get; private set;  }
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

    public void InitCardSprite(Card card)
    {
        this.card = card;
        cardId = card.id;
        title.SetText(card.info.cardName);
        description.SetText(card.info.cardDescription);
        cost.SetText(card.info.cost.ToString());
    }

    private void OnDestroy()
    {
        btn.onClick.RemoveListener(PlayCard);
    }
}
