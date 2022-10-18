using System.Collections.Generic;

public abstract class IngemonState
{
    public BuffsEnum buffType;
    public BuffUIController buffIcon;
    public List<BuffTimings> timings;
    public int duration { set; get; }
    public abstract void SetBuffIcon();
    public abstract int Tick(EntityController target);
}