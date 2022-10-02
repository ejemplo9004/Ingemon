using System.Collections.Generic;
using UnityEngine;

public class GameplayScene : MonoBehaviour
{
    [SerializeField] private Inventory runInventory;
    [SerializeField] protected List<Ingemonster> ingemonesSelected;


    public bool AddIngemonToPreselection(Ingemonster ingemon, int index){
        if(ingemonesSelected.Contains(ingemon)){
            Debug.Log("No agregue ingemones repetidos");
            return false;
        }
        ingemonesSelected[index] = ingemon;
        return true;
    }

    public bool RemoveIngemonFromPreselection(Ingemonster ingemon){
        if(ingemonesSelected.Contains(ingemon)){
            int index = ingemonesSelected.IndexOf(ingemon);
            ingemonesSelected[index] = null;
            Debug.Log("Eliminao");
            return true;
        }
        Debug.Log("No existia el ingemon pa eliminar");
        return false;
    }

    public Inventory RunInventory { get => runInventory; set => runInventory = value; }

}
