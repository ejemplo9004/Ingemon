using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mensajes : MonoBehaviour
{
    public AnimationCurve curvaEntrada;
    public AnimationCurve curvaSalida;
    public RectTransform imMensaje;
    public GameObject imFondo;
    public Text txtMensaje;
    public static Mensajes singleton;
    public int pasos=12;

    void Start()
    {
        singleton = this;
        imMensaje.localScale = Vector3.zero;
        imFondo.SetActive(false);
    }

    public void Popup(string mensaje)
    {
        txtMensaje.text = mensaje;
        StartCoroutine(FadeIn());
    }

    public void Ok()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn()
    {
        imFondo.SetActive(true);
        for (int i = 0; i < pasos; i++)
        {
            imMensaje.localScale = Vector3.one * curvaEntrada.Evaluate((float)i / (pasos-1f));
            yield return new WaitForSeconds(1f / 29f);
        }
    }

    IEnumerator FadeOut()
    {
        for (int i = 0; i < pasos; i++)
        {
            imMensaje.localScale = Vector3.one * curvaSalida.Evaluate((float)i / (pasos-1f));
            yield return new WaitForSeconds(1f / 39f);
        }
        imFondo.SetActive(false);
    }
}
