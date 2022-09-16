using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyReward : MonoBehaviour, IReward
{
    [SerializeField] private PlayerEconomy playerEconomy;
    [SerializeField] private int moneyReward;
    public void AddReward()
    {
        playerEconomy.AddMoney(moneyReward);
    }
}
