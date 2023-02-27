using System.Collections.Generic;
using UnityEngine;
public class Bleed : IngemonState
{
    private int damage;
    private int remaining;

    public Bleed(int damage, int duration, BuffTimings timing)
    {
        this.damage = damage;
        this.duration = duration;
        remaining = duration;
        buffType = BuffsEnum.Bleed;
        timings = new List<BuffTimings> {timing};
    }
    
    public override void SetBuffIcon()
    {
        buffIcon.SetValues(damage, remaining);
    }
    
    public override int Tick(EntityController target)
    {
        target.GetDamageNoProtection(damage);
        remaining--;
        buffIcon.UpdateTimer(remaining);
        return remaining;
    }

    public override void Clear()
    {
        buffIcon.Clear();
    }

    public int DeniedTick()
    {
        remaining--;
        return remaining;
    }
}