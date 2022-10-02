using System;
using UnityEngine;
using TMPro;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private PlayerEconomy playerEconomy;
    [SerializeField] private TMP_InputField ingemonName;
    [SerializeField] private TMP_Text coinsText;

    private void Start()
    {
        playerEconomy.OnMoneyChange.AddListener(delegate { ChangeCoinsInUI(playerEconomy.money); });
        ChangeCoinsInUI(playerEconomy.money);
    }

    private void OnDisable()
    {
        playerEconomy.OnMoneyChange.RemoveListener(delegate { ChangeCoinsInUI(playerEconomy.money); });
    }

    public void EnableIngemonName(bool active)
    {
        if (!active)
        {
            ingemonName.text = "";
        }
        ingemonName.gameObject.SetActive(active);
    }

    public void ChangeCoinsInUI(int coins)
    {
        coinsText.text = "Coins: " + coins;
    }

    public bool VerifyIngemonName()
    {
        if (ingemonName.text.Length > 0)
        {
            return true;
        }
        return false;
    }
    
    public TMP_InputField IngemonName => ingemonName;

}
