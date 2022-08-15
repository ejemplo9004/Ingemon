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

        public void DealDamage(int damage)
        {
            Debug.Log($"{damage} dealt to ");
        }

        public void Heal(int health)
        {
            Debug.Log($"{health} healed to ");
        }

    }

    [System.Serializable]
    public class CardEvent
    {
        public UnityEvent action;
    }
}

