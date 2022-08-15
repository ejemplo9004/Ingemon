using System;
using TMPro;
using UnityEngine;

public class UICombatController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI energyText;

    public void Awake()
    {
        CombatSingletonManager.Instance.eventManager.OnEnergyChange += UpdateEnergyText;
    }

    private void UpdateEnergyText()
    {
        energyText.SetText(
            $"{CombatSingletonManager.Instance.turnManager.info.currentEnergy}/{CombatSingletonManager.Instance.turnManager.info.maxEnergy}");
    }
    
}