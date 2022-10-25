using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardSpriteController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private Image typePanel;
    [SerializeField] private Image panel;
    [SerializeField] private Image frame;
    [SerializeField] private Image divisor;
    [SerializeField] private Image allyTarget;
    [SerializeField] private Image enemyTarget;
    [SerializeField] private Image icon;
    public Sprite[] targets;
    public Card card { get; private set; }
    public int cardId { get; private set; }
    private Button btn;
    public Color32[] colorByOwner;
    public Color32[] colorByBreed;
    public bool isBigCard;

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
        icon.sprite = card.info.sprite;
        SetTargetSprites(card.info.target);
        if (panel != null && CombatSingletonManager.Instance != null)
        {
            SetColorByOwner();
        }
    }
    public int SetOwnerOption()
    {
        CombatInfo info = CombatSingletonManager.Instance.turnManager.info;
        if (card.owner == info.frontAlly)
        {
            return 0;
        }
        else if (card.owner == info.backAlly)
        {
            return 1;
        }
        else if (card.owner == info.frontEnemy)
        {
            return 2;
        }
        else if (card.owner == info.backEnemy)
        {
            return 3;
        }
        return 0;
    }
    public int SetTypeOption()
    {
        CardType ct = card.info.type;
        switch (ct){
            case CardType.ATTACK:
                return 0;
            case CardType.DEFENSE:
                return 1;
            case CardType.DEBUFF:
                return 2;
            case CardType.BUFF:
                return 3;          
            default:
                return 0;
        }
    }
    public int SetBreedOption()
    {
        CombatInfo info = CombatSingletonManager.Instance.turnManager.info;
        if (card.info.race == IngemonRace.GENU)
        {
            return 0;
        }
        else if (card.info.race == IngemonRace.GEDO)
        {
            return 1;
        }
        else if (card.info.race == IngemonRace.GETRE)
        {
            return 2;
        }
        else if (card.info.race == IngemonRace.GECU)
        {
            return 3;
        }
        return 0;
    }
    public void SetColorByOwner()
    {
        if (!(allyTarget && enemyTarget)) return;
        CombatInfo info = CombatSingletonManager.Instance.turnManager.info;
        int owner = SetOwnerOption();
        int breed = SetBreedOption();
        int type = SetTypeOption();
        frame.color = colorByOwner[type];
        panel.color = colorByBreed[type];
        if (isBigCard)
        {
            //divisor.color = colorByBreed[type];
            typePanel.color = colorByBreed[type];
        }
    }
    public void SetTargetSprites(int option)
    {
        if (!(allyTarget && enemyTarget)) return;
        bool isFrontAllyOwner = card.owner == CombatSingletonManager.Instance.turnManager.info.frontAlly;
        if (option == -4)
        {
            option = isFrontAllyOwner ? -1 : -2;
        }
        switch (option)
        {
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
                if (isFrontAllyOwner) allyTarget.sprite = targets[2];
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
        if (btn != null)
            btn.onClick.RemoveListener(PlayCard);
    }
}