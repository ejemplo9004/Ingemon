using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDealDamage
{
    void DealDamage(List<EntityController> objectives);
}

public interface IHeal
{
    void Heal(List<EntityController> objectives);
}

public interface IDraw
{
    void Draw(int draws);
}

public interface IApplyEffect
{
    void ApplyEffect(List<EntityController> objetives, CardModifier modifier);
}

public interface IProtect
{
    void Protect(List<EntityController> objectives, int protection);
}

public interface IDiscard
{
    void Discard(int cards);
}