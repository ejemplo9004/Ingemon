using UnityEngine;

public class ShopSceneController : MonoBehaviour
{
    [SerializeField] private IngemonsterGenerator ingemonGenerator;
    [SerializeField] private CardGenerator cardGenerator;
    public ShopUI shopUI;

    private void Start()
    {
        CheckIngemonCount();
    }

    public void ComeBackToMenu()
    {
        if (!ingemonGenerator.IsInCreation && !cardGenerator.IsInCreation && GameController.gameController.Inventory.Ingemones.Count >= 4)
        {
            SceneChanger.ChangeScene(Scenes.MENU);
        }
        else if(GameController.gameController.Inventory.Ingemones.Count < 4)
        {
            if (Mensajes.singleton != null)
            {
                Mensajes.singleton.Popup("Debe poseer al menos 4 Ingemones");
            }
        }
        else
        {
            Debug.Log("Termine de crear el item");
        }
    }

    private void CheckIngemonCount()
    {
        if(GameController.gameController.Inventory.Ingemones.Count < 4 && Mensajes.singleton != null)
        {
            int count = 4 - GameController.gameController.Inventory.Ingemones.Count;
            Mensajes.singleton.Popup("Para empezar a jugar debes comprar " + count + " Ingemones, presiona alguno de los botones de 'Comprar' para hacerlo");
        }
    }
}
