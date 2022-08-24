using System.Collections.Generic;
using UnityEngine;

public class GameplayScene : MonoBehaviour
{
    [SerializeField] protected Inventory runInventory;
    [SerializeField] protected List<Ingemonster> ingemonesSelected;
    

    public bool AddIngemonToPreselection(Ingemonster ingemon, int index){
        if(ingemonesSelected.Contains(ingemon)){
            Debug.Log("No agregue ingemones repetidos");
            return false;
        }
        ingemonesSelected[index] = ingemon;
        return true;
    }

}
