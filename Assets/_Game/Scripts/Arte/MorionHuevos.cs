using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MorionCambioColores))]
[RequireComponent(typeof(MorionCambioMascaras))]
public class MorionHuevos : MonoBehaviour
{
    public Renderer rendererHuevo;
    [HideInInspector]
    public Material mHuevo;
    MorionCambioColores mcColores;
    MorionCambioMascaras mcMascaras;
    public Texture2D[] manchas;
    // Start is called before the first frame update
    void Awake()
    {
        mHuevo = rendererHuevo.material;
        mcColores = GetComponent<MorionCambioColores>();
        mcMascaras = GetComponent<MorionCambioMascaras>();
    }

    public void Actualizar()
    {
        mHuevo.SetColor("_ColorPiel", mcColores.m.GetColor("_ColorPiel"));
        mHuevo.SetColor("_ColorManchas", mcColores.m.GetColor("_ColorManchas"));
        mHuevo.SetTexture("_Fondo", manchas[mcMascaras.iManchas]);
        mHuevo.SetTextureOffset("_Fondo", Vector2.up * (mcMascaras.iDorsos / 10f));
    }
}