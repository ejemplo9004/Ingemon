using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSceneController : MonoBehaviour
{
    [SerializeField] private IngemonsterGenerator ingemonGenerator;
    [SerializeField] private CardGenerator cardGenerator;

    public void ComeBackToMenu()
    {
        if (!ingemonGenerator.IsInCreation && !cardGenerator.IsInCreation)
        {
            SceneChanger.ChangeScene(0);
        }
        else
        {
            Debug.Log("Termine de crear el item");
        }
    }
}
