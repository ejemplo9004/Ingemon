using UnityEngine;

public class UserCards : MonoBehaviour
{
    private void Start()
    {
        if(!CheckUserCardsBrought()) GetUserCards();
    }

    private bool CheckUserCardsBrought()
    {
        return GameController.gameController.CardInventory.BaseCollection.Count != 0;
    }

    private void GetUserCards()
    {
        //consume servicio y llena las variables
    }
}
