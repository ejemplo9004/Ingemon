using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Economy", menuName = "Ingemon/Economy/Game Economy")]
public class Economy : ScriptableObject
{
    [SerializeField] private List<Item> items;
    
    public List<Item> Items => items;

    public int GetItemCost(string itemName)
    {
        foreach (Item item in items)
        {
            if (item.name.Equals(itemName))
            {
                return item.cost;
            }
        }

        return -1;
    }
}
