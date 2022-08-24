using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IngemonsterGenerator : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private TMP_InputField ingemonName;
    [SerializeField] private MorionCambioPartes partes;
    [SerializeField] private MorionCambioMascaras mascaras;
    [SerializeField] private MorionCambioColores colores;
    private Ingemonster ingemonster;

    public void CreateIngemon(){
        string phenotype = partes.cadena + "-" + mascaras.iDorsos.ToString() + "-" + mascaras.iManchas.ToString() + "-" + colores.numeros;
        ingemonster = new Ingemonster("0", ingemonName.text, phenotype);
        inventory.AddIngemon(ingemonster);
    }
}
