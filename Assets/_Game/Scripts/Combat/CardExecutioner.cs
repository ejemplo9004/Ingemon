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
        List<EntityController> targets = GetTargets(target, owner);
        foreach (var t in targets)
        {
            t.GetDamaged(damage);
        }
        
        CombatSingletonManager.Instance.anim.RequestAnAnimation(
            new AnimationRequestBuilder(owner.position)
                .WithTargets(targets));
        
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

    public void Protect(int protection, int target, EntityController owner)
    {
        foreach (var t in GetTargets(target, owner))
        {
            t.GetProtection(protection);
        }
        CombatSingletonManager.Instance.eventManager.ChangeProtection();
    }

    public void Draw(int cards, EntityController owner)
    {
        if(owner.GetType() == typeof(IngemonController))
            info.handler.Draw(cards);
    }

    public void Poison(int damage, int duration, int target, EntityController owner)
    {
        foreach (var t in GetTargets(target, owner))
        {
            t.SetState(new Poison(damage, duration));
        }
        //CombatSingletonManager.Instance.eventManager.ChangeProtection();
    }

    public void Bleed(int damage, int duration, int target, EntityController owner)
    {
        foreach (var t in GetTargets(target, owner))
        {
            t.SetState(new Bleed(damage, duration));
        }
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
                AddTarget(targets, owner);
                break;
            case Targets.Allys:
                AddTarget(targets, info.frontAlly);
                AddTarget(targets, info.backAlly);
                break;
            case Targets.BackAlly:
                if(!AddTarget(targets, info.backAlly))
                    AddTarget(targets, info.frontAlly);
                break;
            case Targets.FrontAlly:
                if(!AddTarget(targets, info.frontAlly))
                    AddTarget(targets, info.backAlly);
                break;
            case Targets.All:
                AddTarget(targets, info.frontAlly);
                AddTarget(targets, info.backAlly);
                AddTarget(targets, info.backEnemy);
                AddTarget(targets, info.frontEnemy);
                break;
            case Targets.FrontEnemy:
                if(!AddTarget(targets, info.frontEnemy))
                    AddTarget(targets, info.backEnemy);
                break;
            case Targets.BackEnemy:
                if(!AddTarget(targets, info.backEnemy))
                    AddTarget(targets, info.frontEnemy);
                break;
            case Targets.Enemies:
                AddTarget(targets, info.backEnemy);
                AddTarget(targets, info.frontEnemy);
                break;
            case Targets.AllButOneSelf:
                if(owner != info.frontAlly)
                    AddTarget(targets, info.frontAlly);
                if(owner != info.backAlly)
                    AddTarget(targets, info.backAlly);
                if(owner != info.frontEnemy)
                    AddTarget(targets, info.frontEnemy);
                if(owner != info.backEnemy)
                    AddTarget(targets, info.backEnemy);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(target), target, null);
        }
        return targets;
    }

    private bool AddTarget(List<EntityController> targets, EntityController ingemon)
    {
        if (ingemon.CheckDead()) return false;
        targets.Add(ingemon);
        return true;

    }


}