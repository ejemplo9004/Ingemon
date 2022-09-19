[System.Flags]
public enum CardMechanic
{
    DEAL_DAMAGE = 1 << 1,
    HEAL = 1 << 2,
    ADD_PROTECTION = 1 << 3,
    APPLY_EFFECT = 1 << 4,
    DRAW = 1 << 5,
    MODIFY_COST = 1 << 6,
    DISCART = 1 << 7,
    CREATE_CARD = 1 << 8
}

public enum CardObjective
{
    ONESELF,
    ENEMY,
    ENEMIES,
    TEAM,
    ANY
}

public enum CardModifier
{
    DAMAGE_DONE,
    DAMAGE_RECEIVED
}