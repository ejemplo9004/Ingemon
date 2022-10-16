using System.Collections.Generic;
using UnityEngine;

public class EnemiesInventory : ScriptableObject
{
    [SerializeField] private List<Ingemonster> poolOne;

    public Ingemonster GetEnemy()
    {
        int n = poolOne.Count;
        return n > 0 ? poolOne[Random.Range(0, n)] : null;
    }
}