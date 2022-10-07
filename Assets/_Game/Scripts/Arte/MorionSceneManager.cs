using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MorionSceneManager : MonoBehaviour
{
    public Animator animaciones;

    static Animator _anim;
    private void Awake()
    {
        if (_anim == null)
        {
            _anim = animaciones;
            gameObject.name = "CargaEscenas";
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }
    [ContextMenu("Carga De Prueba")]
    public void CambiarEscenaEnsayo()
    {
        MorionSceneManager.LoadScene("Morion");
    }

    public static void LoadScene(string nombreEscena)
    {
        GameObject.Find("CargaEscenas").GetComponent<MorionSceneManager>()._CargarEscena(nombreEscena);
    }

    public void _CargarEscena(string n)
    {
        StartCoroutine(CargarEscena(n));
    }

    IEnumerator CargarEscena(string esce)
    {
        _anim.SetBool("Cargando",true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(esce);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(esce);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        _anim.SetBool("Cargando", false);
    }
}
