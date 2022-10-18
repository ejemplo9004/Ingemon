using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICombatController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI energyText;
    [SerializeField] private Slider frontEnemyHealth;
    [SerializeField] private Slider backEnemyHealth;
    [SerializeField] private Slider frontAllyHealth;
    [SerializeField] private Slider backAllyHealth;
    [SerializeField] public GameObject frontEnemyBUI;
    [SerializeField] public GameObject backEnemyBUI;
    [SerializeField] public GameObject frontAllyBUI;
    [SerializeField] public GameObject backAllyBUI;
    [SerializeField] public float animationTime = 0.5f;
    [SerializeField] private BigCardController bigCard;
    [SerializeField] private IntentionsController intentions;


    public GameObject bleedPrefab;
    public GameObject poisonPrefab;
    public GameObject protectionPrefab;    
    public GameObject partnerProtectionPrefab;
    public GameObject permanentProtectionPrefab;
    public GameObject startProtectionPrefab;

    public void Awake()
    {
        SetHealthBars();
    }

    public void OnEnable()
    {
        CombatSingletonManager.Instance.eventManager.OnEnergyChange += UpdateEnergyText;
        CombatSingletonManager.Instance.eventManager.OnHealthChange += UpdateHealthBars;
        CombatSingletonManager.Instance.eventManager.OnValidCardPlayed += ShowCard;
        CombatSingletonManager.Instance.eventManager.OnEnemyIntentions += SetIntentions;
        CombatSingletonManager.Instance.eventManager.OnIntentionsChange += CleanIntentions;
    }

    private void UpdateEnergyText()
    {
        energyText.SetText(
            $"{CombatSingletonManager.Instance.turnManager.info.energizer.currentEnergy}/{CombatSingletonManager.Instance.turnManager.info.energizer.maxEnergy}");
    }

    public void UpdateHealthBars()
    {
        CombatInfo info = CombatSingletonManager.Instance.turnManager.info;
        StartCoroutine(UpdateHealthBar(frontAllyHealth, frontAllyHealth.value, info.frontAlly.currentHealth));
        StartCoroutine(UpdateHealthBar(backAllyHealth, backAllyHealth.value, info.backAlly.currentHealth));
        StartCoroutine(UpdateHealthBar(frontEnemyHealth, frontEnemyHealth.value, info.frontEnemy.currentHealth));
        StartCoroutine(UpdateHealthBar(backEnemyHealth, backEnemyHealth.value, info.backEnemy.currentHealth));
    }

    private IEnumerator UpdateHealthBar(Slider healthbar, float old, float current)
    {
        float timePassed = 0;
        while (timePassed <= animationTime)
        {
            healthbar.value = Mathf.Lerp(old, current, timePassed / animationTime);
            timePassed += Time.deltaTime;
            yield return null;
        }

        healthbar.value = current;
        yield return null;
    }

    public void SetHealthBars()
    {
        CombatInfo info = CombatSingletonManager.Instance.turnManager.info;
        frontAllyHealth.maxValue = info.frontAlly.ingemonInfo.maxHealth;
        backAllyHealth.maxValue = info.backAlly.ingemonInfo.maxHealth;
        frontEnemyHealth.maxValue = info.frontEnemy.ingemonInfo.maxHealth;
        backEnemyHealth.maxValue = info.backEnemy.ingemonInfo.maxHealth;
    }

    private void SetIntentions(List<Card> cards) => intentions.SetIntentions(cards);
    private void CleanIntentions() => intentions.CleanIntentions();
    private void ShowCard(Card card) => bigCard.AddToShow(card);

    private void OnDisable()
    {
        CombatSingletonManager.Instance.eventManager.OnEnergyChange -= UpdateEnergyText;
        CombatSingletonManager.Instance.eventManager.OnHealthChange -= UpdateHealthBars;
        CombatSingletonManager.Instance.eventManager.OnValidCardPlayed -= ShowCard;
        CombatSingletonManager.Instance.eventManager.OnEnemyIntentions -= SetIntentions;
        CombatSingletonManager.Instance.eventManager.OnIntentionsChange -= CleanIntentions;
    }

    public BuffUIController ShowBuff(CombatIngemonPosition position, BuffsEnum buff)
    {
        GameObject parent = GetBuffParentGameObject(position);
        return Instantiate(GetBuffObject(buff), parent.transform).GetComponent<BuffUIController>();
    }

    private GameObject GetBuffParentGameObject(CombatIngemonPosition position)
    {
        return position switch
        {
            CombatIngemonPosition.FRONT_ALLY => frontAllyBUI,
            CombatIngemonPosition.BACK_ALLY => backAllyBUI,
            CombatIngemonPosition.FRONT_ENEMY => frontEnemyBUI,
            CombatIngemonPosition.BACK_ENEMY => backEnemyBUI,
            _ => throw new ArgumentOutOfRangeException(nameof(position), position, null)
        };
    }

    private GameObject GetBuffObject(BuffsEnum buff)
    {
        return buff switch
        {
            BuffsEnum.Weak => null,
            BuffsEnum.Buffed => null,
            BuffsEnum.Poison => poisonPrefab,
            BuffsEnum.Bleed => bleedPrefab,
            BuffsEnum.Protect => protectionPrefab,
            BuffsEnum.PermanentProtection => permanentProtectionPrefab,
            BuffsEnum.StartProtection => startProtectionPrefab,
            BuffsEnum.PartnerProtection => partnerProtectionPrefab,
            _ => throw new ArgumentOutOfRangeException(nameof(buff), buff, null)
        };
    }

    public void CleanBuffs(CombatIngemonPosition position)
    {
        Transform parent = GetBuffParentGameObject(position).transform;
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
    }
}