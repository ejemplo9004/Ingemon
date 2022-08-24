using System;
using System.Collections.Generic;
using System.Linq;
using Cards;
using UnityEngine;

public class CombatInicializer : MonoBehaviour
{
    public TurnStateManager manager;
    public Inventory combatInventory;
    private IngemonController frontAlly, backAlly;
    private EnemyController frontEnemy, backEnemy;
    public IngemonController baseIngemon;
    public EnemyController baseEnemy;
    public List<ScriptableCard> baseCollection;

    public void Awake()
    {
        //Encontrar mejor manera de agregar las cartas.
        /*ScriptableCard[] cards = Resources.FindObjectsOfTypeAll<ScriptableCard>();
        foreach (var card in cards)
        {
            Debug.Log($"{card.cardName} added!");
            baseCollection.Add(card);
        }*/
        SetIngemons();
    }

    private void SetIngemons()
    {
        frontAlly = Instantiate(baseIngemon, Vector3.zero, Quaternion.identity);
        frontAlly.ingemonInfo = new IngemonBuilder().WithName(combatInventory.Ingemones[0].name)
            .WithMaxHealth(combatInventory.Ingemones[0].maxHealth)
            .WithPhenotype(combatInventory.Ingemones[0].phenotype)
            .WithDeck(baseCollection);
        frontAlly.SetHealth();

        backAlly = Instantiate(baseIngemon, Vector3.zero, Quaternion.identity);
        backAlly.ingemonInfo = new IngemonBuilder().WithName(combatInventory.Ingemones[1].name)
            .WithMaxHealth(combatInventory.Ingemones[1].maxHealth)
            .WithPhenotype(combatInventory.Ingemones[1].phenotype)
            .WithDeck(baseCollection);
        backAlly.SetHealth();

        frontEnemy = Instantiate(baseEnemy, Vector3.zero, Quaternion.identity);
        frontEnemy.ingemonInfo = new IngemonBuilder().WithName("Fishamon")
            .WithMaxHealth(100)
            .WithDeck(baseCollection);
        frontEnemy.SetHealth();

        backEnemy = Instantiate(baseEnemy, Vector3.zero, Quaternion.identity);
        backEnemy.ingemonInfo = new IngemonBuilder().WithName("Corromon")
            .WithMaxHealth(100)
            .WithDeck(baseCollection);
        backEnemy.SetHealth();

        
        manager.info = new CombatInfo(frontAlly, backAlly, frontEnemy, backEnemy, combatInventory);
        (manager.info.drawDeck, manager.info.enemyDeck) = CombatSingletonManager.Instance.cardManager.Init(frontAlly, backAlly, frontEnemy, backEnemy);
        manager.info.drawDeck = manager.info.handler.ShuffleDeck(manager.info.drawDeck);
        manager.info.enemyDeck = manager.info.handler.ShuffleDeck(manager.info.enemyDeck);
    }
}