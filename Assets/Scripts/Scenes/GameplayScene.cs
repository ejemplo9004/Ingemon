using System.Collections.Generic;
using UnityEngine;

public class GameplayScene : MonoBehaviour
{
    [SerializeField] protected GameController gameController;
    [SerializeField] protected Inventory runInventory;
    [SerializeField] protected List<GameObject> ingemonesSelected;
    

    public bool AddIngemonToPreselection(GameObject ingemon, int index){
        if(ingemonesSelected.Contains(ingemon)){
            Debug.Log("No agregue ingemones repetidos");
            return false;
        }
        ingemonesSelected[index] = ingemon;
        return true;
    }

}
