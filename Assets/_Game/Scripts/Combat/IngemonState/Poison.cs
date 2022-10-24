using System.Collections.Generic;
using UnityEngine;

public class Poison : IngemonState
{
    private int remaining;
    private int damage;

    public Poison(int damage, int duration, BuffTimings timing)
    {
        this.damage = damage;
        this.duration = duration;
        remaining = duration;
        buffType = BuffsEnum.Poison;
        timings = new List<BuffTimings> {timing};
    }

    public override void SetBuffIcon()
    {
        buffIcon.SetValues(damage, remaining);
    }

    public override int Tick(EntityController target)
    {
        target.GetDamageNoProtection(damage);
        CombatSingletonManager.Instance.eventManager.ChangeHealth();
        remaining--;
        buffIcon.UpdateTimer(remaining);
        return remaining;
    }

    public override void Clear()
    {
        buffIcon.Clear();
    }
}
