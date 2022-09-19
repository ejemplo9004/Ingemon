using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorionCambioColores : MonoBehaviour
{
    public Gradient coloresPiel;
    public Gradient coloresManchas;
    public Gradient coloresDorsos;
    public Renderer renderer;
    public MeshRenderer[] otrosRenderers;
    [HideInInspector]
    public Material m;
    public string   numeros = "234";
    void Awake()
    {
        m = renderer.material;
        for (int i = 0; i < otrosRenderers.Length; i++)
        {
            otrosRenderers[i].material = m;
        }
    }

    [ContextMenu("Aplicar Color")]
    public void AplicarColores ()
    {
        float c = (float.Parse(numeros[0].ToString()) / 10f);
        m.SetColor("_ColorPiel", coloresPiel.Evaluate(c));
        c = (float.Parse(numeros[1].ToString()) / 10f);
        m.SetColor("_ColorManchas", coloresManchas.Evaluate(c));
        c = (float.Parse(numeros[2].ToString()) / 10f);
        m.SetColor("_ColorDorso", coloresDorsos.Evaluate(c));
    }
    [ContextMenu("ColoresAleatorios")]
    public void AplicarColoresAleatorios()
    {
        numeros = Random.Range(0, 10).ToString() + Random.Range(0, 10).ToString() + Random.Range(0, 10).ToString();
        AplicarColores();
    }
}
