using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnStateManager : MonoBehaviour
{
    public TurnState currentState;
    public StartBattleState startState = new();
    public AllyTurnState allyState = new();
    public EnemyTurnState enemyState = new();
    public WinState winState = new();
    public FailedState failedState = new();
    public CombatInfo info;

    private void Start()
    {
        currentState = startState;
    }

    public void StartBattle(){
        currentState = startState;
        currentState.EnterState(this);
        CombatSingletonManager.Instance.eventManager.OnWinBattle += WonBattle;
        CombatSingletonManager.Instance.eventManager.OnFailBattle += FailedBattle;
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void ChangeState(TurnState newState)
    {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }

    public void WonBattle()
    {
        Unsuscribe();
        ChangeState(winState);
    }

    public void FailedBattle()
    {
        Unsuscribe();
        ChangeState(failedState);
    }

    private void Unsuscribe()
    {
        CombatSingletonManager.Instance.eventManager.OnWinBattle -= WonBattle;
        CombatSingletonManager.Instance.eventManager.OnFailBattle -= FailedBattle;
    }


}
