using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static void ChangeScene(int scene){
        MorionSceneManager.LoadScene(scene);
    }

    public static void ChangeScene(Scenes scene)
    {
        MorionSceneManager.LoadScene((int)scene);
    }
    public static void CambiarEscena(string scene)
    {
        MorionSceneManager.LoadScene(scene);
    }

    public void Salir()
    {
        Application.Quit();
    }
}
