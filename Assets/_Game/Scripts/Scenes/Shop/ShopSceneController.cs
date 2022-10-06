using System.Linq;
using UnityEngine;

public class ShopSceneController : MonoBehaviour
{
    [SerializeField] private IngemonsterGenerator ingemonGenerator;
    [SerializeField] private CardGenerator cardGenerator;
    public ShopUI shopUI;

    public void ComeBackToMenu()
    {
        if (!ingemonGenerator.IsInCreation && !cardGenerator.IsInCreation && GameController.gameController.Inventory.Ingemones.Count >= 4)
        {
            SceneChanger.ChangeScene(Scenes.MENU);
        }
        else
        {
            Debug.Log("Termine de crear el item");
        }
    }
}
