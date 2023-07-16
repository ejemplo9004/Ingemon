using System;
using System.Collections.Generic;
using System.Linq;
using Cards;
using UnityEngine;

public class RunCombatInitializer : MonoBehaviour
{
    public TurnStateManager manager;
    public Inventory combatInventory;
    public EnemiesInventory enemiesInventory;
    private IngemonController frontAlly, backAlly;
    private EnemyController frontEnemy, backEnemy;
    public IngemonController baseIngemon;
    public EnemyController baseEnemy;
    public List<ScriptableCard> baseCollection;

    public void Awake()
    {
        baseCollection = RunSingleton.Instance.runDeck.Deck;
        SetIngemons();
    }

    private void InstantiateIngemons()
    {
        frontAlly = Instantiate(baseIngemon, Vector3.zero, Quaternion.identity);
        backAlly = Instantiate(baseIngemon, Vector3.zero, Quaternion.identity);
    }

    private void SetIngemons()
    {
        InstantiateIngemons();

        List<Ingemonster> ingemones = new List<Ingemonster>();
        if (combatInventory != null)
        {
            ingemones.Add(new IngemonBuilder(combatInventory.Ingemones[0].id).WithName(combatInventory.Ingemones[0].name)
                .WithMaxHealth(combatInventory.Ingemones[0].maxHealth)
                .WithPhenotype(combatInventory.Ingemones[0].phenotype)
                .WithDeck(baseCollection));
            
            ingemones.Add(new IngemonBuilder(combatInventory.Ingemones[1].id).WithName(combatInventory.Ingemones[1].name)
                .WithMaxHealth(combatInventory.Ingemones[1].maxHealth)
                .WithPhenotype(combatInventory.Ingemones[1].phenotype)
                .WithDeck(baseCollection));
        }
        else
        {
            ingemones = new List<Ingemonster>();
            ingemones.Add(new IngemonBuilder().WithName("Firulaismon").WithMaxHealth(100).WithPhenotype("1-3-6-3-3-465").WithDeck(baseCollection));
            ingemones.Add(new IngemonBuilder().WithName("Cachimon").WithMaxHealth(100).WithPhenotype("4-1-2-9-6-312").WithDeck(baseCollection));
        }

        frontAlly.ingemonInfo = ingemones[0];
        frontAlly.SetUI(CombatIngemonPosition.FRONT_ALLY);

        backAlly.ingemonInfo = ingemones[1];
        backAlly.SetUI(CombatIngemonPosition.BACK_ALLY);

        //Cambiar para agregar enemigos
        frontEnemy = Instantiate(baseEnemy, Vector3.zero, Quaternion.identity);
        frontEnemy.ingemonInfo = enemiesInventory.GetEnemy();
        frontEnemy.SetUI(CombatIngemonPosition.FRONT_ENEMY);

        backEnemy = Instantiate(baseEnemy, Vector3.zero, Quaternion.identity);
        backEnemy.ingemonInfo = enemiesInventory.GetEnemy();
        backEnemy.SetUI(CombatIngemonPosition.BACK_ENEMY);

        
        manager.info = new CombatInfo(frontAlly, backAlly, frontEnemy, backEnemy, combatInventory);
        SetCardsOnManagerInfo();
    }

    public void SetCardsOnManagerInfo()
    {
        (manager.info.drawDeck, manager.info.enemyDeck) = CombatSingletonManager.Instance.cardManager.Init(frontAlly, backAlly, frontEnemy, backEnemy);
        manager.info.drawDeck = manager.info.handler.ShuffleDeck(manager.info.drawDeck);
        manager.info.enemyDeck = manager.info.handler.ShuffleDeck(manager.info.enemyDeck);
    }

    public void HackTheGame(int code)
    {
        manager.info.HackBattle(code);
    }
}