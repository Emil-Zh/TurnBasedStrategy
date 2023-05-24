using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private int MAX_ACTION_POINTS = 2;

    [SerializeField] private bool isEnemy;
    public bool IsEnemy { get { return isEnemy; } }

    public static event EventHandler OnAnyPointsChanged;
    public GridPosition CurrentGridPosition { get; private set; }
    public MoveAction MoveAction { get; private set; }
    public SpinAction SpinAction { get; private set; }


    private BaseAction[] baseActionArray;
    private HealthSystem healthSystem;
    private int actionPoints;

    public int ActionPoints { get { return actionPoints; } }

    private void Awake()
    {
        MoveAction = GetComponent<MoveAction>();
        SpinAction = GetComponent<SpinAction>();
        baseActionArray = GetComponents<BaseAction>();
        healthSystem = GetComponent<HealthSystem>();
        actionPoints = MAX_ACTION_POINTS;

    }

    private void Start()
    {
        CurrentGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(CurrentGridPosition, this);
        TurnSystem.Instance.OnTurnChange += TurnSystem_OnTurnChange;
        healthSystem.OnDead += HealthSystem_OnDead;
    }
    private void Update()
    {
        CheckGridPosition();
    }

    private void CheckGridPosition()
    {
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newGridPosition != CurrentGridPosition)
        {
            LevelGrid.Instance.UnitMoveGridPosition(this, CurrentGridPosition, newGridPosition);
            CurrentGridPosition = newGridPosition;
        }
    }

    public BaseAction[] GetBaseActionArray() => baseActionArray;

    private bool CanSpendActionPointsToTakeAction(BaseAction baseAction)
    {
        return actionPoints >= baseAction.GetActionPointsCost();
    }

    private void SpendActionPoints(int amount)
    {
        actionPoints -= amount;

        OnAnyPointsChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool TrySpendActionPointsToTakeAction(BaseAction baseAction)
    {
        if (!CanSpendActionPointsToTakeAction(baseAction))
        {
            return false;
        }
        SpendActionPoints(baseAction.GetActionPointsCost());
        return true;
    }

    private void TurnSystem_OnTurnChange(object sender, EventArgs e)
    {
        if (isEnemy && !TurnSystem.Instance.IsPlayerTurn ||
            !isEnemy && TurnSystem.Instance.IsPlayerTurn)
        {
            actionPoints = MAX_ACTION_POINTS;

            OnAnyPointsChanged?.Invoke(this, EventArgs.Empty);
        }
    }


    public void TakeDamage(int damageAmount)
    {
      healthSystem.TakeDamage(damageAmount);
    }

    private void HealthSystem_OnDead(object sender, EventArgs e)
    {
        LevelGrid.Instance.RemoveUnitAtGridPosition(CurrentGridPosition, this);
        Destroy(gameObject);
    }




}
