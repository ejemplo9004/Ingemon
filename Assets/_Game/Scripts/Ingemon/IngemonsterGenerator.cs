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
    [SerializeField] private Economia economia;
    [SerializeField] private IngemonCreationVisuals ingemonCreationVisuals;
    [SerializeField] private float eggFadeDuration;
    [SerializeField] private float ingemonShowDuration;
    private Ingemonster ingemonster;
    [SerializeField] public Vector2Int healthRange = new Vector2Int(45,55);
    private bool isInCreation;
    private List<GameObject> morionHuevosList;
    private List<int> morionHuevosRaza;
    private GameObject eggSelected;
    [SerializeField] private Vector3 ingemonSelectedPosition;
    [SerializeField] private Camera ingemonSelectedCamera;
    private int eggIndex;
    private void Start()
    {
        morionHuevosList = new List<GameObject>();
        morionHuevosRaza = new List<int>();
        GenerateEggs();
    }
    public void GenerateEggs()
    {
        int buttonIndex = 0;
        if (morionHuevosList.Count != 0)
        {
            foreach (GameObject egg in morionHuevosList)
            {
                shopScene.shopUI.RemoveButtonListeners(buttonIndex);
                buttonIndex++;
                Destroy(egg);
            }
            morionHuevosList.Clear();
            morionHuevosRaza.Clear();
            eggSelected = null;
        }
        for (int i = 0; i < 4; i++)
        {
            int index = Random.Range(0, eggsPrefabs.Count);
            GameObject eggCopy = Instantiate(eggsPrefabs[index], eggsPositions[i], Quaternion.Euler(0f, 180f, 0));
            eggCopy.GetComponent<MorionCambioPartes>().Aleatorizar();
            eggCopy.GetComponent<MorionHuevos>().VerHuevo(true, true);
            morionHuevosList.Add(eggCopy);
            morionHuevosRaza.Add(index);
        }

        buttonIndex = 0;
        foreach (GameObject huevo in morionHuevosList)  
        {
            shopScene.shopUI.AddButtonListeners(delegate{CreateIngemon(huevo, morionHuevosRaza[morionHuevosList.IndexOf(huevo)]);}, buttonIndex);
            buttonIndex++;
        }
    }
    public void CreateIngemon(GameObject ingemon, int raza)
    {
        if (isInCreation)
        {
            return;
        }
        int ingemonCost = gameEconomy.GetItemCost("Ingemon");
        if (playerEconomy.VerifyBuy(ingemonCost))
        {
            eggSelected = ingemon;
            eggIndex = raza;
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
            int deck = 0;
            switch (eggIndex)
            {
                case 0:
                    deck = Random.Range(0, GameController.gameController.CardSet1.Count);
                    break;
                case 1:
                    deck = Random.Range(0, GameController.gameController.CardSet2.Count);
                    break;
                case 2:
                    deck = Random.Range(0, GameController.gameController.CardSet3.Count);
                    break;
                case 3:
                    deck = Random.Range(0, GameController.gameController.CardSet4.Count);
                    break;
                default:
                    break;
            }

            string phenotype = partes.cadena + "-" + mascaras.iDorsos + "-" + mascaras.iManchas + "-" + colores.numeros + "-" + eggIndex + "-" + deck;
            int defaultHealth = Random.Range(healthRange.x, healthRange.y);
            ingemonster = new IngemonBuilder().WithName(shopScene.shopUI.IngemonName.text).WithPhenotype(phenotype).WithMaxHealth(defaultHealth);
            economia.ingemonNuevo = ingemonster;
            economia.comprarObjeto(playerEconomy.money);
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

        return new Color(imageColor.r, imageColor.g, imageColor.b, 1f);
    }
    public bool IsInCreation => isInCreation;
}