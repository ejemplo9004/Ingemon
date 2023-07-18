using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MorionSceneManager : MonoBehaviour
{
    public Animator animaciones;
    public UnityEngine.UI.Text texto;

    static Animator _anim;
    static UnityEngine.UI.Text _texto;
    public AudioSource audioS;
    public int FPS = 30;
    private void Awake()
    {
        if (_anim == null)
        {
            _anim = animaciones;
            _texto = texto;
            gameObject.name = "CargaEscenas";
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }

        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = FPS;
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
    public static void LoadScene(int numero)
    {
        GameObject.Find("CargaEscenas").GetComponent<MorionSceneManager>()._CargarEscena(numero);
    }

    public void _CargarEscena(string n)
    {
        StartCoroutine(CargarEscena(n));
    }

    public void _CargarEscena(int n)
    {
        StartCoroutine(CargarEscena(n));
    }

    IEnumerator CargarEscena(string esce)
    {
        _anim.SetBool("Cargando",true);
        if (_texto != null && ObtenerFrases.singleton.frasesF.Count > 0)
        {
            _texto.text = ObtenerFrases.singleton.frasesF[Random.Range(0, ObtenerFrases.singleton.frasesF.Count)];
        }
        yield return new WaitForSeconds((Application.isEditor) ? 0.1f : 1.5f);
        audioS.Play();
        SceneManager.LoadScene(esce);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(esce);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        _anim.SetBool("Cargando", false);
        yield return new WaitForSeconds((Application.isEditor) ? 0.1f : 2.5f);
        //_texto.text = "";
    }


    IEnumerator CargarEscena(int esce)
    {
        _anim.SetBool("Cargando", true);
        if (_texto != null && ObtenerFrases.singleton.frasesF.Count > 0)
        {
            _texto.text = ObtenerFrases.singleton.frasesF[Random.Range(0, ObtenerFrases.singleton.frasesF.Count)];
        }
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene(esce);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(esce);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        _anim.SetBool("Cargando", false);
        yield return new WaitForSeconds(2.5f);
        _texto.text = "";
    }
}
