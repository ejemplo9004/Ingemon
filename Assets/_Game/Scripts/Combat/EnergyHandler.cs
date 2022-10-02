using UnityEngine;

public class EnergyHandler
{
    public int currentEnergy, maxEnergy;

    public EnergyHandler(int maxEnergy)
    {
        this.maxEnergy = maxEnergy;
    }
    
    public void SpendEnergy(int spent)
    {
        currentEnergy -= spent;
        CombatSingletonManager.Instance.eventManager.ChangeEnergy();
    }
    
    public void ResetEnergy()
    {
        currentEnergy = maxEnergy;
        CombatSingletonManager.Instance.eventManager.ChangeEnergy();
    }
    
    public bool IsPlayable(Card card) => currentEnergy >= card.info.cost;
}