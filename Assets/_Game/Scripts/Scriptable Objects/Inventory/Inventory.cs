using System.Collections.Generic;
using UnityEngine;
using System;
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
            //Persistence.persistence.SaveIngemon(ingemones, ingemones.Count);
            return;            
        }
        else if(ingemones.Count == limit){
            Debug.Log("No se pueden asignar mas ingemones");
            return;
        }
        Ingemonster ingemonCopy = new Ingemonster(ingemon);
        ingemones.Add(ingemonCopy);
        
    }

    public bool DeleteIngemon(string ingemon){
        if(ingemones.Count == 0){
            return false;
        }

        foreach (Ingemonster ingemonster in ingemones)
        {
            if (ingemonster.phenotype.Equals(ingemon))
            {
                ingemones.Remove(ingemonster);
                return true;
            }
        }
        return false;
    }

    public void ClearInventory(){
        ingemones.Clear();
    }

    public List<Ingemonster> Ingemones { get => ingemones; set => ingemones = value; }
}
