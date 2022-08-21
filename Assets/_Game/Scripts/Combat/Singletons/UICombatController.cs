using System;
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

    public void Awake()
    {
        Debug.Log($"Hellow There {this}");
        CombatSingletonManager.Instance.eventManager.OnEnergyChange += UpdateEnergyText;
        CombatSingletonManager.Instance.eventManager.OnHealthChange += UpdateHealthBars;
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
    
}