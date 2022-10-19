using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardSpriteController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private Image costPanel;
    [SerializeField] private Image panel;
    [SerializeField] private Image allyTarget;
    [SerializeField] private Image enemyTarget;
    public Sprite[] targets;
    public Card card { get; private set;  }
    public int cardId { get; private set;  }
    private Button btn;
    private Color32 frontColor = new (114, 186, 110, 255);
    private Color32 backColor = new (113, 174, 245, 255);

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
        SetTargetSprites(card.info.target);
        if (panel != null && CombatSingletonManager.Instance != null)
        {
            if (card.owner == CombatSingletonManager.Instance.turnManager.info.frontAlly)
            {
                panel.color = frontColor;
                costPanel.color = frontColor;
            }
            else
            {
                panel.color = backColor;
                costPanel.color = backColor;
            }
        }
    }

    public void SetTargetSprites(int option)
    {
        if(!(allyTarget && enemyTarget)) return;
        bool isFrontAllyOwner = card.owner == CombatSingletonManager.Instance.turnManager.info.frontAlly;
        if (option == -4){
            option = isFrontAllyOwner? -1 : -2;
        }
        switch (option){
            case -3:
                allyTarget.sprite = targets[6];
                enemyTarget.sprite = targets[1];
            break;
            case -2:
                allyTarget.sprite = targets[4];
                enemyTarget.sprite = targets[1];
            break;   
            case -1:
                allyTarget.sprite = targets[2];
                enemyTarget.sprite = targets[1];
            break;
            case 0:
                allyTarget.sprite = targets[6];
                enemyTarget.sprite = targets[7];
            break;                                 
            case 1:
                allyTarget.sprite = targets[0];
                enemyTarget.sprite = targets[3];
            break; 
            case 2:
                allyTarget.sprite = targets[0];
                enemyTarget.sprite = targets[5];
            break; 
            case 3:
                allyTarget.sprite = targets[0];
                enemyTarget.sprite = targets[7];
            break;
            case 4:
                if(isFrontAllyOwner) allyTarget.sprite = targets[2];
                else allyTarget.sprite = targets[4];
                enemyTarget.sprite = targets[7];
            break;
            default:
                allyTarget.sprite = targets[0];
                enemyTarget.sprite = targets[1];
            break;                                               
        }

    }
    private void OnDestroy()
    {
        if(btn != null)
            btn.onClick.RemoveListener(PlayCard);
    }
}
