using System.Collections.Generic;

public class DeadController
{
    private HashSet<EntityController> deadIngemons;

    public DeadController()
    {
        deadIngemons = new HashSet<EntityController>();
    }

    public void LetIngemonDie()
    {
        if(deadIngemons.Count < 1) return;
        
        foreach (var ingemon in deadIngemons)
        {
            CombatSingletonManager.Instance.turnManager.info.PurgeCardsFromDeckAfterAnIngemonDie(ingemon);
            ingemon.DeadAnimation();
            ingemon.CleanBuffs();
        }
        
        deadIngemons.Clear();
    }

    public void AddDeadIngemon(EntityController deadIngemon)
    {
        deadIngemons.Add(deadIngemon);
    }
}