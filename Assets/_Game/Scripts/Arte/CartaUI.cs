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


}

[System.Serializable]
public enum TipoObjetivo
{
    Ninguno = 0,
    Frente  = 1,
    Atras   = 2,
    Ambos   = 3
}