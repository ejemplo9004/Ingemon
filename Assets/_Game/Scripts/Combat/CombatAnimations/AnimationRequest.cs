using System.Collections.Generic;

public class AnimationRequest
{
    public List<CombatIngemonPosition> attackers { get; }
    public List<CombatIngemonPosition> targets { get; }

    public AnimationRequest(CombatIngemonPosition attacker, CombatIngemonPosition target)
    {
        attackers = new List<CombatIngemonPosition> { attacker };
        targets = new List<CombatIngemonPosition> { target };
    }

    public AnimationRequest(List<CombatIngemonPosition> attackers, List<CombatIngemonPosition> targets)
    {
        this.attackers = new List<CombatIngemonPosition>();
        this.targets = new List<CombatIngemonPosition>();
        this.attackers.AddRange(attackers);
        this.targets.AddRange(targets);
    }

    public AnimationRequest(CombatIngemonPosition actor)
    {
        attackers = new List<CombatIngemonPosition> { actor };
        targets = new List<CombatIngemonPosition>();
    }

    public List<CombatIngemonPosition> GetActors()
    {
        List<CombatIngemonPosition> actors = new List<CombatIngemonPosition>();
        actors.AddRange(attackers);
        actors.AddRange(targets);
        return actors;
    }

    public bool IsActor(CombatIngemonPosition ingemon)
    {
        return attackers.Contains(ingemon) || targets.Contains(ingemon);
    }

    public bool AreActors(List<CombatIngemonPosition> ingemones)
    {
        foreach (CombatIngemonPosition ingemon in ingemones)
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
        foreach (CombatIngemonPosition attacker in attackers)
        {
            EntityController ingemon = info.GetIngemon(attacker);
            ingemon.AttackAnimation();
        }
        
        foreach (CombatIngemonPosition target in targets)
        {
            EntityController ingemon = info.GetIngemon(target);
            ingemon.DamageAnimation();
        }
    }
}