using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Cards
{
    [CreateAssetMenu(fileName = "Card", menuName = "Ingemon/Card", order = 1)]
    public class ScriptableCard : ScriptableObject
    {
        public string id;
        public string cardName;
        public string cardDescription;
        public int cost;
        public IngemonRace race;
        public CardEvent[] actions;


        public void PlayCard()
        {
            foreach (CardEvent action in actions)
            {
                action.action.Invoke();
            }
        }

        public void Target(Hellow target)
        {
            Debug.Log(target);
        }

        public void DealDamage(int damage)
        {
            Debug.Log($"{damage} dealt to ");
        }

        public void Heal(int health)
        {
            Debug.Log($"{health} healed to ");
        }

        public void TestoEvent(CombatInfo inf)
        {
            Debug.Log("Hello");
        }

    }

    [System.Serializable]
    public class CardEvent
    {
        public UnityEvent action;
        public EntityController target;
    }
    
    public enum Targets{
        Oneself,
        Allys,
        AllAllys,
        OneEnemy,
        AllEnemies,
        All
    }

    public class Hellow : ScriptableObject
    {
        public int heloooo;
    }
}

