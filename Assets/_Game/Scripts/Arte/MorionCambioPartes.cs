using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MorionCambioMascaras))]

public class MorionCambioPartes : MonoBehaviour
{
    MorionCambioMascaras mCambioMascaras;
    [Header("Accesorios")]
    public GameObject[] cabeza;
    public GameObject[] manos;
    public GameObject[] cuerpo;

    public string cadena;


    int iCabeza;
    int iManos;
    int iCuerpo;

    void Awake()
    {
        mCambioMascaras = GetComponent<MorionCambioMascaras>();
    }

    void AplicarCambios()
    {
        for (int i = 0; i < 10; i++)
        {
            cabeza[i].SetActive (i == iCabeza);
            manos[i].SetActive  (i == iManos);
            cuerpo[i].SetActive (i == iCuerpo);
        }

        cadena = iCabeza.ToString() + "-" + iManos.ToString() + "-" + iCuerpo.ToString();
    }

    [ContextMenu("Aleatorizar")]
    public void Aleatorizar()
    {
        iCabeza = Random.Range(0,10);
        iManos  = Random.Range(0,10);
        iCuerpo = Random.Range(0,10);
        AplicarCambios();
        mCambioMascaras.Aleatorizar();
    }

    public void TransformarIngemon(int _cabeza, int _manos, int _cuerpo){
        iCabeza = _cabeza;
        iManos = _manos;
        iCuerpo = _cuerpo;
        AplicarCambios();
    }
}
