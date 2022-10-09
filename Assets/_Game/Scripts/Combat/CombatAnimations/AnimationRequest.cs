using System.Collections.Generic;

public class AnimationRequest
{
    public List<CombatIngemonEnum> attackers { get; }
    public List<CombatIngemonEnum> targets { get; }

    public AnimationRequest(CombatIngemonEnum attacker, CombatIngemonEnum target)
    {
        attackers = new List<CombatIngemonEnum> { attacker };
        targets = new List<CombatIngemonEnum> { target };
    }

    public AnimationRequest(List<CombatIngemonEnum> attackers, List<CombatIngemonEnum> targets)
    {
        this.attackers = new List<CombatIngemonEnum>();
        this.targets = new List<CombatIngemonEnum>();
        this.attackers.AddRange(attackers);
        this.targets.AddRange(targets);
    }

    public AnimationRequest(CombatIngemonEnum actor)
    {
        attackers = new List<CombatIngemonEnum> { actor };
        targets = new List<CombatIngemonEnum>();
    }

    public List<CombatIngemonEnum> GetActors()
    {
        List<CombatIngemonEnum> actors = new List<CombatIngemonEnum>();
        actors.AddRange(attackers);
        actors.AddRange(targets);
        return actors;
    }

    public bool IsActor(CombatIngemonEnum ingemon)
    {
        return attackers.Contains(ingemon) || targets.Contains(ingemon);
    }

    public bool AreActors(List<CombatIngemonEnum> ingemones)
    {
        foreach (CombatIngemonEnum ingemon in ingemones)
        {
            if (IsActor(ingemon))
            {
                return true;
            }
        }

        return false;
    }

    public void ExecuteAnimation(CombatInfo info)
    {
        foreach (CombatIngemonEnum attacker in attackers)
        {
            EntityController ingemon = info.GetIngemon(attacker);
            ingemon.AttackAnimation();
        }
        
        foreach (CombatIngemonEnum target in targets)
        {
            EntityController ingemon = info.GetIngemon(target);
            ingemon.DamageAnimation();
        }
    }
}