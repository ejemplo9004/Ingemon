using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Ingemon/Economy/Player Economy", fileName = "Player Economy")]
public class PlayerEconomy : ScriptableObject
{
    public int money;
    public int errorCost = -1;
    [SerializeField] private UnityEvent onMoneyChange;

    public bool VerifyBuy(int cost)
    {
        if (cost == errorCost)
        {
            Debug.Log("El item buscado no se encontró");
            return false;
        }
        if (money >= cost)
        {
            SetPlayerMoney(money - cost);
            return true;
        }

        Debug.Log("No hay money");
        return false;
    }

    public void AddMoney(int amount)
    {
        SetPlayerMoney(money + amount);
    }

    private void SetPlayerMoney(int amount)
    {
        money = amount;
        onMoneyChange.Invoke();
    }

    public UnityEvent OnMoneyChange => onMoneyChange;
}
