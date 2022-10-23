using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSingletonManager : MonoBehaviour
{
    public static CombatSingletonManager Instance;
    
    #region Singleton

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    #endregion

    public CombatEventSystem eventManager;
    public TurnStateManager turnManager;
    public CardCombatController cardManager;
    public UICombatController uiManager;
    public AnimationExecuter anim;
    public int damageMultiplier = 1;


}
