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

        private int target = 1;
        private EntityController owner;


        public void PlayCard(EntityController owner)
        {
            this.owner = owner;
            foreach (CardEvent action in actions)
            {
                action.action.Invoke();
            }
        }

        public void SetTarget(int target)
        {
            this.target = target;
        }

        public void DealDamage(int damage)
        {
            Debug.Log($"{damage} dealt to {(Targets)target}");
            CombatSingletonManager.Instance.turnManager.info.executioner.DealDamage(damage, target, owner);
        }

        public void Heal(int health)
        {
            Debug.Log($"{health} healed to {(Targets)target}");
            CombatSingletonManager.Instance.turnManager.info.executioner.Heal(health, target, owner);
        }

        public void Draw(int cards)
        {
            Debug.Log($"Draw {cards} cards");
            CombatSingletonManager.Instance.turnManager.info.executioner.Draw(cards, owner);
        }

        public void Protect(int value)
        {
            Debug.Log($"You protect {value} damage");
        }
    }

    [System.Serializable]
    public class CardEvent
    {
        public UnityEvent action;
    }
}