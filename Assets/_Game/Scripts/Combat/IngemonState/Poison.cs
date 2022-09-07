using UnityEngine;

public class Poison : IngemonState
{
    public int duration { set; get; }
    private int remaining;
    public int damage { set; get; }

    public Poison(int damage, int duration)
    {
        this.damage = damage;
        this.duration = duration;
        remaining = duration;
        buffType = BuffsEnum.POISON;
    }

    public override void SetBuffIcon()
    {
        buffIcon.SetValues(damage, remaining);
    }

    public override int Tick(EntityController target)
    {
        target.GetDamageNoProtection(damage);
        CombatSingletonManager.Instance.eventManager.ChangeHealth();
        Debug.Log($"{target.ingemonInfo.name} {target.position} get poison for {damage} damage.");
        remaining--;
        buffIcon.UpdateTimer(remaining);
        return remaining;
    }
}
