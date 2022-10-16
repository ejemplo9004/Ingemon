using UnityEngine;

public class IngemonDetails : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    public void CreateInfo(int index)
    {
        Ingemonster ingemon = inventory.Ingemones[index];
        
    }
}
