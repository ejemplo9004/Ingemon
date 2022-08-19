using System;
using System.Collections.Generic;
using System.Linq;
using Cards;
using UnityEngine;

public class CombatInicializer : MonoBehaviour
{
    public TurnStateManager manager;
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
        Debug.Log($"EL TOTAL DE CARTAS ES {baseCollection.Count}");
        SetIngemons();
    }

    private void SetIngemons()
    {
        frontAlly = Instantiate(baseIngemon, Vector3.zero, Quaternion.identity);
        frontAlly.ingemonInfo = new IngemonBuilder().WithName("Karumon")
            .WithMaxHealth(100)
            .WithDeck(baseCollection);
        frontAlly.SetHealth();
        
        backAlly = Instantiate(baseIngemon, Vector3.zero, Quaternion.identity);
        backAlly.ingemonInfo = new IngemonBuilder().WithName("Flagymon")
            .WithMaxHealth(100)
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
        
        Debug.Log($"HOLA a todos {CombatSingletonManager.Instance.cardManager != null}");
        
        manager.info = new CombatInfo(frontAlly, backAlly, frontEnemy, backEnemy);
        (manager.info.drawDeck, manager.info.enemyDeck) = CombatSingletonManager.Instance.cardManager.Init(frontAlly, backAlly, frontEnemy, backEnemy);
        manager.info.handler.ShuffleDeck();
    }
}