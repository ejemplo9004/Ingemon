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
    [SerializeField] private BigCardController bigCard;
    [SerializeField] private IntentionsController intentions;

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

    private void UpdateHealthBars()
    {
        CombatInfo info = CombatSingletonManager.Instance.turnManager.info;
        frontAllyHealth.value = info.frontAlly.currentHealth;
        backAllyHealth.value = info.backAlly.currentHealth;
        frontEnemyHealth.value = info.frontEnemy.currentHealth;
        backEnemyHealth.value = info.backEnemy.currentHealth;
    }

    private void SetHealthBars()
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
}