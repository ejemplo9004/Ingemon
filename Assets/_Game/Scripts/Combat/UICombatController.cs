using System;
using TMPro;
using UnityEngine;


public class UICombatController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI energyText;

    public void Start()
    {
        CombatEventSystem.Instance.OnEnergyChange += UpdateEnergyText;
    }

    public void UpdateEnergyText()
    {
        energyText.SetText($"{TurnStateManager.Instance.info.currentEnergy}/{TurnStateManager.Instance.info.maxEnergy}");
    }
    
    
}