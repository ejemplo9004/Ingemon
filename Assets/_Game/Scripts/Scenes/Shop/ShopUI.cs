using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private PlayerEconomy playerEconomy;
    [SerializeField] private Economy gameEconomy;
    [SerializeField] private TMP_InputField ingemonName;
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private List<Button> buyButtons;
    [SerializeField] private GameObject eggsImage;
    [SerializeField] private GameObject ingemonImage;
    [SerializeField] private GameObject currentSection;
    private RawImage ingemonRawImage;
    private bool isInTransaction;

    private void Start()
    {
        playerEconomy.OnMoneyChange.AddListener(delegate { ChangeCoinsInUI(playerEconomy.money); });
        ChangeCoinsInUI(playerEconomy.money);
        SetItemPrice("Ingemon");
        ingemonRawImage = ingemonImage.GetComponent<RawImage>();
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

    public void SetItemPrice(string item)
    {
        if (isInTransaction)
        {
            return;
        }
        switch (item)
        {
            case "Ingemon":
                priceText.text = "precio: " + gameEconomy.GetItemCost("Ingemon").ToString() + " monedas";
                break;
            case "Card":
                priceText.text = "precio: " + gameEconomy.GetItemCost("Card").ToString() + " monedas";
                break;
            default:
                break;
        }
    }

    public bool VerifyIngemonName()
    {
        if (ingemonName.text.Length > 0)
        {
            return true;
        }
        return false;
    }

    public void AddButtonListeners(Action method, int index)
    {
        buyButtons[index].onClick.AddListener(delegate { method(); });
    }

    public void RemoveButtonListeners(Action method, int index)
    {
        buyButtons[index].onClick.RemoveAllListeners();
    }

    public void EnableBornUI(bool enable)
    {
        eggsImage.SetActive(!enable);
        foreach (Button button in buyButtons)
        {
            button.gameObject.SetActive(!enable);
        }
        ingemonImage.SetActive(enable);
        isInTransaction = enable;
    }

    public void OpenSection(GameObject section)
    {
        if (!isInTransaction)
        {
            section.SetActive(true);
            currentSection.SetActive(false);
            currentSection = section;
        }
    }
    
    public TMP_InputField IngemonName => ingemonName;
    public RawImage IngemonImage => ingemonRawImage;
}
