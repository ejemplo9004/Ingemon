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

    public void DealDamage(int damage, int target, int baseBonus, EntityController owner, int modifier)
    {
        Debug.Log($"Daño Base: {damage}. \nObjectivo: {target}.\nModifier: {modifier}");
        List<EntityController> targets = GetTargets(target, owner);
        foreach (var t in targets)
        {
            damage = ModifyDamage(damage, modifier, baseBonus, t, owner);
            Debug.Log($"Daño total: {damage}. \nObjectivo: {t.position}.\nModifier: {modifier}");
            t.GetDamaged(damage);
        }

        CombatSingletonManager.Instance.anim.RequestAnAnimation(
            new AnimationRequestBuilder(owner.position)
                .WithTargets(targets));

        CombatSingletonManager.Instance.eventManager.ChangeHealth();
        CombatSingletonManager.Instance.turnManager.info.deadController.LetIngemonDie();
    }

    public void Heal(int health, int target, EntityController owner)
    {
        foreach (var t in GetTargets(target, owner))
        {
            Debug.Log($"Curando a {t.position}");
            t.GetHealed(health);
        }
        owner.MagicAnimation();
        CombatSingletonManager.Instance.eventManager.ChangeHealth();
    }

    public void Protect(int protection, int target, EntityController owner)
    {
        foreach (var t in GetTargets(target, owner))
        {
            t.GetProtection(protection);
        }
        owner.MagicAnimation();
        CombatSingletonManager.Instance.eventManager.ChangeProtection();
    }

    public void ClearProtection(int target, EntityController owner)
    {
        foreach (var t in GetTargets(target, owner))
        {
            t.ClearProtection();
        }

        CombatSingletonManager.Instance.eventManager.ChangeProtection();
    }

    public void Draw(int cards, EntityController owner)
    {
        if (owner.GetType() == typeof(IngemonController))
            info.handler.Draw(cards);
    }

    public void Poison(int damage, int duration, int target, EntityController owner)
    {
        foreach (var t in GetTargets(target, owner))
        {
            BuffTimings timing = t.GetType() == typeof(IngemonController)
                ? BuffTimings.AllyEndTurn
                : BuffTimings.EnemyEndTurn;
            t.SetState(new Poison(damage, duration, timing));
        }
    }

    public void HealPoison(int target, EntityController owner)
    {
        foreach (EntityController t in GetTargets(target, owner))
        {
            t.CleanPoison();
        }
        owner.MagicAnimation();
    }

    public void Bleed(int damage, int duration, int target, EntityController owner)
    {
        foreach (var t in GetTargets(target, owner))
        {
            BuffTimings timing = t.GetType() == typeof(IngemonController)
                ? BuffTimings.AllyEndTurn
                : BuffTimings.EnemyEndTurn;
            t.SetState(new Bleed(damage, duration, timing));
        }
    }

    private void SetOtherState(int value, int duration, BuffsEnum modifier, int target, EntityController owner)
    {
        foreach (var t in GetTargets(target, owner))
        {
            bool ally = t.GetType() == typeof(IngemonController);
            BuffTimings timing;
            switch (modifier)
            {
                case BuffsEnum.Weak:
                    break;
                case BuffsEnum.Buffed:
                    break;
                case BuffsEnum.PermanentProtection:
                    timing = ally ? BuffTimings.EnemyStartTurn : BuffTimings.AllyStartTurn;
                    if (!t.IsBuffedWith(BuffsEnum.PermanentProtection))
                        t.SetState(new PermanentProtection(duration, timing));
                    break;
                case BuffsEnum.StartProtection:
                    timing = ally ? BuffTimings.AllyStartTurn : BuffTimings.EnemyStartTurn;
                    if (t.IsBuffedWith(BuffsEnum.StartProtection))
                    {
                        t.UpdateState(BuffsEnum.StartProtection, value);
                    }
                    else
                    {
                        t.SetState(new StartProtection(value, duration, timing));
                    }

                    break;
                case BuffsEnum.PartnerProtection:
                    timing = ally ? BuffTimings.AllyStartTurn : BuffTimings.EnemyStartTurn;
                    if (!t.IsBuffedWith(BuffsEnum.PartnerProtection))
                        t.SetState(new PartnerProtection(duration, timing));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(modifier), modifier, null);
            }
        }
        owner.MagicAnimation();
    }

    public void SetState(int value, int duration, int modifier, int target, EntityController owner)
    {
        switch ((BuffsEnum)modifier)
        {
            case BuffsEnum.Weak:
                break;
            case BuffsEnum.Buffed:
                break;
            case BuffsEnum.Poison:
                Poison(value, duration, target, owner);
                break;
            case BuffsEnum.Bleed:
                Bleed(value, duration, target, owner);
                break;
            case BuffsEnum.Protect:
                Protect(value, target, owner);
                break;
            case BuffsEnum.PermanentProtection:
                SetOtherState(value, duration, BuffsEnum.PermanentProtection, target, owner);
                break;
            case BuffsEnum.StartProtection:
                SetOtherState(value, duration, BuffsEnum.StartProtection, target, owner);
                break;
            case BuffsEnum.PartnerProtection:
                SetOtherState(value, duration, BuffsEnum.PartnerProtection, target, owner);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(modifier), modifier, null);
        }
    }

    public void HealBleed(int target, EntityController owner)
    {
        foreach (EntityController t in GetTargets(target, owner))
        {
            t.CleanBleed();
        }
        owner.MagicAnimation();
    }

    public List<EntityController> GetTargets(int target, EntityController owner)
    {
        if (CombatSingletonManager.Instance.turnManager.currentState.GetType() == typeof(EnemyTurnState))
        {
            if (target < 4 && target > -4)
                target = -target;
        }

        List<EntityController> targets = new List<EntityController>();
        switch ((Targets)target)
        {
            case Targets.OneSelf:
                AddTarget(targets, owner);
                break;
            case Targets.Allys:
                AddTarget(targets, info.frontAlly);
                AddTarget(targets, info.backAlly);
                break;
            case Targets.BackAlly:
                if (CombatSingletonManager.Instance.turnManager.currentState.GetType() == typeof(EnemyTurnState))
                {
                    if (info.backAlly.IsBuffedWith(BuffsEnum.PartnerProtection))
                    {
                        if (!AddTarget(targets, info.frontAlly))
                        {
                            AddTarget(targets, info.backAlly);
                        }

                        break;
                    }
                }

                if (!AddTarget(targets, info.backAlly))
                    AddTarget(targets, info.frontAlly);
                break;
            case Targets.FrontAlly:
                if (CombatSingletonManager.Instance.turnManager.currentState.GetType() == typeof(EnemyTurnState))
                {
                    if (info.frontAlly.IsBuffedWith(BuffsEnum.PartnerProtection))
                    {
                        if (!AddTarget(targets, info.backAlly))
                        {
                            AddTarget(targets, info.frontAlly);
                        }

                        break;
                    }
                }

                if (!AddTarget(targets, info.frontAlly))
                    AddTarget(targets, info.backAlly);
                break;
            case Targets.All:
                AddTarget(targets, info.frontAlly);
                AddTarget(targets, info.backAlly);
                AddTarget(targets, info.backEnemy);
                AddTarget(targets, info.frontEnemy);
                break;
            case Targets.FrontEnemy:
                if (CombatSingletonManager.Instance.turnManager.currentState.GetType() == typeof(AllyTurnState))
                {
                    if (info.frontEnemy.IsBuffedWith(BuffsEnum.PartnerProtection))
                    {
                        if (!AddTarget(targets, info.backEnemy))
                        {
                            AddTarget(targets, info.frontEnemy);
                        }

                        break;
                    }
                }

                if (!AddTarget(targets, info.frontEnemy))
                {
                    AddTarget(targets, info.backEnemy);
                }

                break;
            case Targets.BackEnemy:
                if (CombatSingletonManager.Instance.turnManager.currentState.GetType() == typeof(AllyTurnState))
                {
                    if (info.backEnemy.IsBuffedWith(BuffsEnum.PartnerProtection))
                    {
                        if (!AddTarget(targets, info.frontEnemy))
                        {
                            AddTarget(targets, info.backEnemy);
                        }

                        break;
                    }
                }

                if (!AddTarget(targets, info.backEnemy))
                {
                    AddTarget(targets, info.frontEnemy);
                }

                break;
            case Targets.Enemies:
                AddTarget(targets, info.backEnemy);
                AddTarget(targets, info.frontEnemy);
                break;
            case Targets.AllButOneSelf:
                if (owner != info.frontAlly)
                    AddTarget(targets, info.frontAlly);
                if (owner != info.backAlly)
                    AddTarget(targets, info.backAlly);
                if (owner != info.frontEnemy)
                    AddTarget(targets, info.frontEnemy);
                if (owner != info.backEnemy)
                    AddTarget(targets, info.backEnemy);
                break;
            case Targets.Partner:
                if (owner.position == CombatIngemonPosition.FRONT_ALLY)
                {
                    AddTarget(targets, info.backAlly);
                }
                else
                {
                    AddTarget(targets, info.frontAlly);
                }
                break;
            case Targets.EnemyPartner:
                if (owner.position == CombatIngemonPosition.FRONT_ENEMY)
                {
                    AddTarget(targets, info.backEnemy);
                }
                else
                {
                    AddTarget(targets, info.frontEnemy);
                }
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

    private int ModifyDamage(int damage, int modifier, int baseBonus, EntityController target, EntityController owner)
    {
        switch ((DamageModifiers)modifier)
        {
            case DamageModifiers.OwnerArmorEqualsDamage:
                damage = owner.protection;
                break;
            case DamageModifiers.TargetPoisonBonus:
                damage += target.IsPoisoned() ? baseBonus : 0;
                break;
            case DamageModifiers.TargetBleedBonus:
                damage += target.IsBleeding() ? baseBonus : 0;
                break;
            case DamageModifiers.TargetPoisonOrBleedBonus:
                damage += (target.IsBleeding() || target.IsPoisoned()) ? baseBonus : 0;
                break;
            case DamageModifiers.OwnerPoison:
                damage += owner.IsPoisoned() ? baseBonus : 0;
                break;
            case DamageModifiers.OwnerBleed:
                damage += owner.IsBleeding() ? baseBonus : 0;
                break;
            case DamageModifiers.OwnerPoisonOrBleed:
                damage += (owner.IsBleeding() || owner.IsPoisoned()) ? baseBonus : 0;
                break;
            case DamageModifiers.OwnerArmorModifyDamage:
                damage += owner.protection * baseBonus;
                break;
        }

        return damage;
    }

    public void Discard(int cards, int modifier, EntityController owner)
    {
        if (owner.GetType() != typeof(IngemonController)) return;
        switch ((DiscardModifiers)modifier)
        {
            case DiscardModifiers.DiscardRandom:
                for (int i = 0; i < cards; i++)
                {
                    info.handler.DiscardRandom();
                }

                break;
            case DiscardModifiers.DiscardExpensive:
                for (int i = 0; i < cards; i++)
                {
                    info.handler.DiscardExpensive();
                }

                break;
        }
    }
}