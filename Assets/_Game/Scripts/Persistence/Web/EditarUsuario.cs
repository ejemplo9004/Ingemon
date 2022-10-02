using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditarUsuario : MonoBehaviour
{
    public Servidor servidor;
    public InputField inpUsuarioEditar;
    public InputField inpPassEditar;
    public InputField inpPass2;
    public InputField inpJugadorEditar;
    public InputField inpNivelEditar;
    public GameObject imLoading;

    public void editarUsuario()
    {
        StartCoroutine(Editar());
    }
    IEnumerator Editar()
    {
        imLoading.SetActive(true);
        string[] datos = new string[5];
        datos[0] = inpUsuarioEditar.text;
        datos[1] = inpPassEditar.text;
        datos[2] = inpPass2.text;
        datos[3] = inpJugadorEditar.text;
        datos[4] = inpNivelEditar.text;
        StartCoroutine(servidor.ConsumirServicio("editar usuario", datos, null));
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => !servidor.ocupado);
        imLoading.SetActive(false);
    }
}