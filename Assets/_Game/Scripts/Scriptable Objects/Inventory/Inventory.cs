using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Inventory", menuName = "Ingemon/Inventory")]
public class Inventory : ScriptableObject
{
    [SerializeField] private List<Ingemonster> ingemones;
    [SerializeField] private bool hasLimit;
    [SerializeField] private int limit;
    public void AddIngemon(Ingemonster ingemon){
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

    public void DeleteIngemon(Ingemonster ingemon){
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

    public List<Ingemonster> Ingemones { get => ingemones; set => ingemones = value; }
}
