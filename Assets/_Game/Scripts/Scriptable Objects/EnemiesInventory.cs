using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyInventory", menuName = "Ingemon/EnemyInventory")]
public class EnemiesInventory : ScriptableObject
{
    [SerializeField] private List<EnemyObject> pool_one;

    public Ingemonster GetEnemy()
    {
        int n = pool_one.Count;
        if (n > 0)
        {
            return pool_one[Random.Range(0, n)].Get();
        }

        return new IngemonBuilder().WithName("BasicMon")
            .WithMaxHealth(100)
            .WithPhenotype("1-3-6-3-3-465-2");
    }
}