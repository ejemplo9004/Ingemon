using System.Collections.Generic;

public class PartnerProtection : IngemonState
{
    private int remaining;

    public PartnerProtection( int duration, BuffTimings timing )
    {
        this.duration = duration;
        remaining = duration;
        buffType = BuffsEnum.PartnerProtection;
        timings = new List<BuffTimings> {timing};
    }
    
    public override void SetBuffIcon()
    {
        buffIcon.SetValues(0, remaining);
    }
    
    public override int Tick(EntityController target)
    {
        if(remaining == -1) return -1;
        remaining--;
        buffIcon.UpdateTimer(remaining);
        return remaining;
    }

    public override void Clear()
    {
        timings = null;
        buffIcon.Clear();
    }
}