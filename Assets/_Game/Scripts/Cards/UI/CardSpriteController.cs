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
    private Color32 frontColor = new (67, 255, 0, 57);
    private Color32 backColor = new (0, 112, 255, 57);

    public void Start()
    {
        btn = GetComponent<Button>();
        // if(btn != null)
        //     btn.onClick.AddListener(PlayCard);
    }

    public void PlayCard()
    {
        CombatSingletonManager.Instance.eventManager.PlayCard(card);
    }

    public void InitCardSprite(Card card)
    {
        this.card = card;
        cardId = card.id;
        title.SetText(card.info.cardName);
        description.SetText(card.info.cardDescription);
        cost.SetText(card.info.cost.ToString());
        if (panel != null && CombatSingletonManager.Instance != null)
        {
            if (card.owner == CombatSingletonManager.Instance.turnManager.info.frontAlly)
            {
                panel.color = frontColor;
            }
            else
            {
                panel.color = backColor;
            }
        }
    }

    private void OnDestroy()
    {
        if(btn != null)
            btn.onClick.RemoveListener(PlayCard);
    }
}
