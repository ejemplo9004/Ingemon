using UnityEngine;

public class HealthReward : MonoBehaviour, IReward
{
    [SerializeField] private RoomController roomController;
    [Range(0, 100)]
    [SerializeField] private float healthPercent;
    public void AddReward()
    {
        foreach (Ingemonster ingemon in roomController.RunInventory.Ingemones)
        {
            float health = ingemon.maxHealth * healthPercent/100;
            ingemon.maxHealth += (int)health;
        }
    }
}
