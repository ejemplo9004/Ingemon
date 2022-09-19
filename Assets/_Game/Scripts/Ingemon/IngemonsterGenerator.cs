using UnityEngine;
using TMPro;

public class IngemonsterGenerator : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private PlayerEconomy playerEconomy;
    [SerializeField] private Economy gameEconomy;
    [SerializeField] private ShopUI shopUI;
    [SerializeField] private MorionCambioPartes partes;
    [SerializeField] private MorionCambioMascaras mascaras;
    [SerializeField] private MorionCambioColores colores;
    [SerializeField] private GameObject ingemonModel;
    [SerializeField] private Comprar comprar;
    private Ingemonster ingemonster;
    public int defaultHealth;
    private bool isInCreation;
    
    public void CreateIngemon()
    {
        if (isInCreation)
        {
            return;
        }
        int ingemonCost = gameEconomy.GetItemCost("Ingemon");
        if (playerEconomy.VerifyBuy(ingemonCost))
        {
            partes.Aleatorizar();
            ingemonModel.SetActive(true);
            shopUI.EnableIngemonName(true);
            isInCreation = true;
        }
    }

    public void SaveIngemon()
    {
        if (shopUI.VerifyIngemonName())
        {
            string phenotype = partes.cadena + "-" + mascaras.iDorsos.ToString() + "-" + mascaras.iManchas.ToString() + "-" + colores.numeros;
            ingemonster = new IngemonBuilder().WithName(shopUI.IngemonName.text).WithPhenotype(phenotype).WithMaxHealth(defaultHealth);
            comprar.ingemonNuevo = ingemonster;
            comprar.comprarObjeto(playerEconomy.money);
            inventory.AddIngemon(ingemonster);
            shopUI.EnableIngemonName(false);
            ingemonModel.SetActive(false);
            isInCreation = false;
        }
        else
        {
            Debug.Log("Ponle un nombre al ingemon, gracias");
        }
    }

    public bool IsInCreation => isInCreation;
}
