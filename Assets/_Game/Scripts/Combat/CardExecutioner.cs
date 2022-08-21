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

    public void DealDamage(int damage, int target)
    {
        foreach (var t in GetTargets((Targets)target))
        {
            t.GetDamaged(damage);
        }
        CombatSingletonManager.Instance.eventManager.ChangeHealth();
    }

    public void Heal(int health, int target)
    {
        foreach (var t in GetTargets((Targets)target))
        {
            t.GetHealed(health);
        }
        CombatSingletonManager.Instance.eventManager.ChangeHealth();
    }

    public void Draw(int cards)
    {
        info.handler.Draw(cards);
    }

    public List<EntityController> GetTargets(Targets target)
    {
        List<EntityController> targets = new List<EntityController>();
        switch (target)
        {
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
            case Targets.AllEnemies:
                targets.Add(info.backEnemy);
                targets.Add(info.frontEnemy);
                break;
            case Targets.Oneself:
                break;
            case Targets.Allys:
                targets.Add(info.frontAlly);
                targets.Add(info.backAlly);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(target), target, null);
        }
        Debug.Log("Getting Targets");
        return targets;
    }
}