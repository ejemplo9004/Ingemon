using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthReward : MonoBehaviour, IReward
{
    [SerializeField] private RunController runController;
    [Range(0, 100)]
    [SerializeField] private float healthPercent;
    public void AddReward()
    {
        foreach (Ingemonster ingemon in runController.RunInventory.Ingemones)
        {
            float health = ingemon.maxHealth * healthPercent/100;
            ingemon.maxHealth += (int)health;
        }
    }
}
