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
    }
    
    public int Tick(EntityController target)
    {
        target.GetDamageNoProtection(damage);
        CombatSingletonManager.Instance.eventManager.ChangeHealth();
        Debug.Log($"{target.ingemonInfo.name} get bleeded for {damage} damage.");
        remaining--;
        return remaining;
    }

    public int DeniedTick()
    {
        remaining--;
        return remaining;
    }
}