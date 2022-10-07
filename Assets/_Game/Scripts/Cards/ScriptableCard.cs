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
        public CardType type;
        public CardEvent[] actions;

        private int target = 1;
        private int duration = 0;
        private EntityController owner;


        public void PlayCard(EntityController owner)
        {
            this.owner = owner;
            foreach (CardEvent action in actions)
            {
                action.action.Invoke();
            }
        }

        public void SetTarget(int target) => this.target = target;
        public void SetDuration(int duration) => this.duration = duration;

        public void DealDamage(int damage)
        {
            CombatSingletonManager.Instance.turnManager.info.executioner.DealDamage(damage, target, owner);
            owner.AttackAnimation();
        }

        public void Heal(int health)
        {
            CombatSingletonManager.Instance.turnManager.info.executioner.Heal(health, target, owner);
        }

        public void Draw(int cards)
        {
            CombatSingletonManager.Instance.turnManager.info.executioner.Draw(cards, owner);
        }

        public void Protect(int protection)
        {
            CombatSingletonManager.Instance.turnManager.info.executioner.Protect(protection, target, owner);
        }
        public void Poison(int damage)
        {
            CombatSingletonManager.Instance.turnManager.info.executioner.Poison(damage, duration, target, owner);
            duration = 0;
        }
        public void Bleed(int damage)
        {
            CombatSingletonManager.Instance.turnManager.info.executioner.Bleed(damage, duration, target, owner);
            duration = 0;
        }
        
        
        
        
    }

    [System.Serializable]
    public class CardEvent
    {
        public UnityEvent action;
    }
}