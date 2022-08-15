using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnStateManager : MonoBehaviour
{
    public static TurnStateManager Instance { get; private set; }
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
    
    public TurnState currentState;
    public StartBattleState startState = new();
    public AllyTurnState allyState = new();
    public CardResolveState resolveState = new();
    public EnemyTurnState enemyState = new();
    public EndBattleState endState = new();
    public CombatInfo info;

    private void Start()
    {
        currentState = startState;
        currentState.EnterState(this);
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

}
