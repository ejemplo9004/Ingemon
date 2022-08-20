using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Inventory", menuName = "Ingemon/Inventory")]
public class Inventory : ScriptableObject
{
    [SerializeField] private List<GameObject> ingemones;
    [SerializeField] private bool hasLimit;
    [SerializeField] private int limit;
    public void AddIngemon(GameObject ingemon){
        if(!hasLimit){
            ingemones.Add(ingemon);
            return;
        }
        else if(ingemones.Count == limit){
            Debug.Log("No se pueden asignar mas ingemones");
            return;
        }
        ingemones.Add(ingemon);
    }

    public void DeleteIngemon(GameObject ingemon){
        if(ingemones.Count == 0){
            Debug.Log("No hay ingemones para borrar.");
            return;
        }
        if(ingemones.Contains(ingemon)){
            ingemones.Remove(ingemon);
            Debug.Log("Ingemon removido");
        }
        else{
            Debug.Log("No se encontro el Ingemon para borrar.");
        }
    }

    public void ClearInventory(){
        ingemones.Clear();
    }

    public List<GameObject> Ingemones { get => ingemones; set => ingemones = value; }
}
