using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Ingemon/Enemy")]
public class EnemyObject : ScriptableObject
{
    [SerializeField] private string EnemyName;
    [SerializeField] private int health;
    [SerializeField] private string phenotype = "0-0-0-0-0-000-0";
    [SerializeField] private CardSet cardSet;

    public Ingemonster Get()
    {
        return new IngemonBuilder().WithName(EnemyName)
            .WithMaxHealth(health)
            .WithPhenotype(phenotype)
            .WithDeck(cardSet.Get());
    }
}