using System.Collections.Generic;

public class AnimationRequestBuilder
{
    public AnimationRequest request;

    public AnimationRequestBuilder(CombatIngemonPosition mainActor)
    {
        request = new AnimationRequest(mainActor);
    }

    public AnimationRequestBuilder WithAlly(CombatIngemonPosition actor)
    {
        if(!request.IsActor(actor))
            request.attackers.Add(actor);
        return this;
    }
    
    public AnimationRequestBuilder WithTarget(CombatIngemonPosition actor)
    {
        if(!request.IsActor(actor))
            request.targets.Add(actor);
        return this;
    }

    public AnimationRequestBuilder WithTargets(List<CombatIngemonPosition> actors)
    {
        foreach (CombatIngemonPosition actor in actors)
        {
            if(!request.IsActor(actor))
                request.targets.Add(actor);
        }
        return this;
    }
    
    public AnimationRequestBuilder WithTargets(List<EntityController> actors)
    {
        foreach (EntityController actor in actors)
        {
            if(!request.IsActor(actor.position))
                request.targets.Add(actor.position);
        }
        return this;
    }

    public static implicit operator AnimationRequest(AnimationRequestBuilder arb)
    {
        return arb.request;
    }
}