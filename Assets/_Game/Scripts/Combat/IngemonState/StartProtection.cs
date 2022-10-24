using System.Collections.Generic;
using UnityEngine;

public class StartProtection : IngemonState, IUpdatableState
{
    private int remaining;
    private int protection;
    
    public StartProtection(int protection, int duration, BuffTimings timing)
    {
        this.duration = duration;
        this.protection = protection;
        remaining = duration;
        buffType = BuffsEnum.StartProtection;
        timings = new List<BuffTimings> {timing};
    }

    public override void SetBuffIcon()
    {
        buffIcon.SetValues(protection, remaining);
    }

    public override int Tick(EntityController target)
    {
        target.GetProtection(protection);
        CombatSingletonManager.Instance.eventManager.ChangeProtection();
        if(remaining == -1) return -1;
        remaining--;
        buffIcon.UpdateTimer(remaining);
        return remaining;
    }

    public override void Clear()
    {
        buffIcon.Clear();
    }

    public void UpdateState(int value)
    {
        protection += value;
        SetBuffIcon();
    }
}