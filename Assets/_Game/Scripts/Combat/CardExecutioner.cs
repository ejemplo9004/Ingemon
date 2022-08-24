using System;
using System.Collections.Generic;
using UnityEngine;


public class CardExecutioner
{
    private CombatInfo info;

    public CardExecutioner(CombatInfo info)
    {
        this.info = info;
    }

    public void DealDamage(int damage, int target, EntityController owner)
    {
        foreach (var t in GetTargets(target, owner))
        {
            t.GetDamaged(damage);
        }
        CombatSingletonManager.Instance.eventManager.ChangeHealth();
    }

    public void Heal(int health, int target, EntityController owner)
    {
        foreach (var t in GetTargets(target, owner))
        {
            t.GetHealed(health);
        }
        CombatSingletonManager.Instance.eventManager.ChangeHealth();
    }

    public void Draw(int cards, EntityController owner)
    {
        if(owner.GetType() == typeof(IngemonController))
            info.handler.Draw(cards);
    }

    public List<EntityController> GetTargets(int target, EntityController owner)
    {
        if (CombatSingletonManager.Instance.turnManager.currentState.GetType() == typeof(EnemyTurnState))
        {
            if(target != 4 && target != -4)
                target = -target;
        }
        List<EntityController> targets = new List<EntityController>();
        switch ( (Targets) target )
        {
            case Targets.OneSelf:
                targets.Add(owner);
                break;
            case Targets.Allys:
                targets.Add(info.frontAlly);
                targets.Add(info.backAlly);
                break;
            case Targets.BackAlly:
                targets.Add(info.backAlly);
                break;
            case Targets.FrontAlly:
                targets.Add(info.frontAlly);
                break;
            case Targets.All:
                targets.Add(info.frontAlly);
                targets.Add(info.backAlly);
                targets.Add(info.backEnemy);
                targets.Add(info.frontEnemy);
                break;
            case Targets.FrontEnemy:
                targets.Add(info.frontEnemy);
                break;
            case Targets.BackEnemy:
                targets.Add(info.backEnemy);
                break;
            case Targets.Enemies:
                targets.Add(info.backEnemy);
                targets.Add(info.frontEnemy);
                break;
            case Targets.AllButOneSelf:
                if(owner != info.frontAlly)
                    targets.Add(info.frontAlly);
                if(owner != info.backAlly)
                    targets.Add(info.backAlly);
                if(owner != info.frontEnemy)
                    targets.Add(info.frontEnemy);
                if(owner != info.backEnemy)
                    targets.Add(info.backEnemy);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(target), target, null);
        }
        return targets;
    }
}