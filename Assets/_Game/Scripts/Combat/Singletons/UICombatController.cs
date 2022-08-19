using System;
using TMPro;
using UnityEngine;

public class UICombatController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI energyText;

    public void Awake()
    {
        Debug.Log($"Hellow There {this}");
        CombatSingletonManager.Instance.eventManager.OnEnergyChange += UpdateEnergyText;
    }

    private void UpdateEnergyText()
    {
        energyText.SetText(
            $"{CombatSingletonManager.Instance.turnManager.info.currentEnergy}/{CombatSingletonManager.Instance.turnManager.info.maxEnergy}");
    }
    
}