using UnityEngine;
public class Bleed : IngemonState
{
    public int damage { get; set; }
    public int duration { get; set; }
    private int remaining;

    public Bleed(int damage, int duration)
    {
        this.damage = damage;
        this.duration = duration;
        remaining = duration;
        buffType = BuffsEnum.BLEED;
    }
    
    public override void SetBuffIcon()
    {
        buffIcon.SetValues(damage, remaining);
    }
    
    public override int Tick(EntityController target)
    {
        target.GetDamageNoProtection(damage);
        CombatSingletonManager.Instance.eventManager.ChangeHealth();
        Debug.Log($"{target.ingemonInfo.name} get bleeded for {damage} damage.");
        remaining--;
        buffIcon.UpdateTimer(remaining);
        return remaining;
    }

    public int DeniedTick()
    {
        remaining--;
        return remaining;
    }
}