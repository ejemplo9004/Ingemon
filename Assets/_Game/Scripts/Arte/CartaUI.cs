using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CartaUI : MonoBehaviour
{
    public Color[] coloresBordes;
    public Color[] coloresFondos;
    public Sprite[] sprsAliados;
    public Sprite[] sprsEnemigos;

    public Image imFondo;
    public Image[] imBordes;
    public Text txtTitulo;
    public Text txtFrase;
    public Text txtDescripcion;
    public Image imEquipoAliado;
    public Image imEquipoEnemigo;

    public void PonerEnemigos(TipoObjetivo tipo)
    {
        imEquipoEnemigo.sprite = sprsEnemigos[(int)tipo];
    }
    public void PonerAliados(TipoObjetivo tipo)
    {
        imEquipoAliado.sprite = sprsAliados[(int)tipo];
    }

    public void CabiarColorBordes(int cual)
    {
        for (int i = 0; i < imBordes.Length; i++)
        {
            imBordes[i].color = coloresBordes[cual % coloresBordes.Length];
        }
    }
    public void CabiarColorFondo(int cual)
    {
        imFondo.color = coloresFondos[cual % coloresBordes.Length];
    }
}

[System.Serializable]
public enum TipoObjetivo
{
    Ninguno = 0,
    Frente  = 1,
    Atras   = 2,
    Ambos   = 3
}