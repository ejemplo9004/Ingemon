public abstract class IngemonState
{
    public BuffsEnum buffType;
    public BuffUIController buffIcon;
    public abstract int Tick(EntityController target);
    public abstract void SetBuffIcon();
}