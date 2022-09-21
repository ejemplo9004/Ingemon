using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class IngemonsterGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> eggsPrefabs;
    [SerializeField] private List<Vector3> eggsPositions;
    [SerializeField] private ShopSceneController shopScene;
    [SerializeField] private Inventory inventory;
    [SerializeField] private PlayerEconomy playerEconomy;
    [SerializeField] private Economy gameEconomy;
    [SerializeField] private Comprar comprar;
    [SerializeField] private IngemonCreationVisuals ingemonCreationVisuals;
    [SerializeField] private float eggFadeDuration;
    [SerializeField] private float ingemonShowDuration;
    private Ingemonster ingemonster;
    public int defaultHealth;
    private bool isInCreation;
    private List<GameObject> morionHuevosList;
    private GameObject eggSelected;
    [SerializeField] private Vector3 ingemonSelectedPosition;
    [SerializeField] private Camera ingemonSelectedCamera;
    private void Start()
    {
        morionHuevosList = new List<GameObject>();
        GenerateEggs();
    }

    private void Update()
    {

    }

    public void GenerateEggs()
    {
        int buttonIndex = 0;
        if (morionHuevosList.Count != 0)
        {
            foreach (GameObject egg in morionHuevosList)
            {
                shopScene.shopUI.RemoveButtonListeners(delegate{CreateIngemon(egg);}, buttonIndex);
                buttonIndex++;
                Destroy(egg);
            }
            morionHuevosList.Clear();
            eggSelected = null;
        }
        for (int i = 0; i < 4; i++)
        {
            int index = Random.Range(0, eggsPrefabs.Count);
            GameObject eggCopy = Instantiate(eggsPrefabs[index], eggsPositions[i], Quaternion.Euler(0f, 180f, 0));
            eggCopy.GetComponent<MorionCambioPartes>().Aleatorizar();
            eggCopy.GetComponent<MorionHuevos>().VerHuevo(true, true);
            morionHuevosList.Add(eggCopy);
        }

        buttonIndex = 0;
        foreach (GameObject huevo in morionHuevosList)  
        {
            shopScene.shopUI.AddButtonListeners(delegate{CreateIngemon(huevo);}, buttonIndex);
            buttonIndex++;
        }
    }
    public void CreateIngemon(GameObject ingemon)
    {
        if (isInCreation)
        {
            return;
        }
        int ingemonCost = gameEconomy.GetItemCost("Ingemon");
        if (playerEconomy.VerifyBuy(ingemonCost))
        {
            eggSelected = ingemon;
            shopScene.shopUI.EnableBornUI(true);
            StartCoroutine(IngemonBorn());
        }
    }
    public void SaveIngemon()
    {
        if (shopScene.shopUI.VerifyIngemonName())
        {
            MorionCambioColores colores = eggSelected.GetComponent<MorionCambioColores>();
            MorionCambioMascaras mascaras = eggSelected.GetComponent<MorionCambioMascaras>();
            MorionCambioPartes partes = eggSelected.GetComponent<MorionCambioPartes>();
            string phenotype = partes.cadena + "-" + mascaras.iDorsos.ToString() + "-" + mascaras.iManchas.ToString() + "-" + colores.numeros;
            ingemonster = new IngemonBuilder().WithName(shopScene.shopUI.IngemonName.text).WithPhenotype(phenotype).WithMaxHealth(defaultHealth);
            comprar.ingemonNuevo = ingemonster;
            comprar.comprarObjeto(playerEconomy.money);
            inventory.AddIngemon(ingemonster);
            shopScene.shopUI.EnableIngemonName(false);
            isInCreation = false;
        }
        else
        {
            Debug.Log("Ponle un nombre al ingemon, gracias");
        }
    }
    private IEnumerator IngemonBorn()
    {
        eggSelected.transform.position = ingemonSelectedPosition;
        ingemonSelectedCamera.gameObject.SetActive(true);
        RawImage image = shopScene.shopUI.IngemonImage;
        StartCoroutine(ingemonCreationVisuals.FadeEgg(image, image.color, GetEndColor(true), eggFadeDuration));
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => ingemonCreationVisuals.IsFaded);
        MorionHuevos egg = eggSelected.GetComponent<MorionHuevos>();
        egg.VerHuevo(false, false);
        StartCoroutine(ingemonCreationVisuals.FadeEgg(image, image.color, GetEndColor(false), ingemonShowDuration));
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => ingemonCreationVisuals.IsFaded);
        shopScene.shopUI.EnableIngemonName(true);
        isInCreation = true;
        yield return new WaitUntil(() => !isInCreation);
        shopScene.shopUI.EnableBornUI(false);
        GenerateEggs();
    }

    private Color GetEndColor(bool fade)
    {
        Color imageColor = shopScene.shopUI.IngemonImage.color;
        if (fade)
        {
            return new Color(imageColor.r, imageColor.g, imageColor.b, 0f);
        }

        return new Color(imageColor.r, imageColor.g, imageColor.b, 255f);
    }
    public bool IsInCreation => isInCreation;
}