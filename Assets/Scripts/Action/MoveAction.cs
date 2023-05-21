using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveAction : BaseAction
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private int maxMoveDistance = 4;
    Animator unitAnimator;
    string isWalkingAnimation = "IsWalking";
    private Vector3 targetPosition;
    
   
    protected override void Awake()
    {
        base.Awake();
        unitAnimator = GetComponentInChildren<Animator>();
        targetPosition = transform.position;
    }
    private void Update()
    {
        if(!isActive)
        {
            return;
        }
        MoveToTarget();
    }
    private void MoveToTarget()
    {
        Vector3 moveDirection = (targetPosition - transform.position).normalized;
        float stoppingDisntacce = 0.1f;
        float distanceBetweenUnitAndTarget = Vector3.Distance(transform.position, targetPosition);
        if (distanceBetweenUnitAndTarget >= stoppingDisntacce)
        {
            
            transform.position += moveDirection * Time.deltaTime * moveSpeed;
            ChangeAnimationState(isWalkingAnimation, true);
        }
        else
        {
            
            ChangeAnimationState(isWalkingAnimation, false);
            ActionComplete();
        }
        Rotate(moveDirection);

    }

    public override void TakeAction(GridPosition target, Action onActionComplete)
    {
        ActionStart(onActionComplete);
        targetPosition = LevelGrid.Instance.GetWorldPosition(target);
        
    }
    private void Rotate(Vector3 rotationDirection)
    {
        transform.forward = Vector3.Lerp(transform.forward, rotationDirection, Time.deltaTime * rotationSpeed);
    }
    private void ChangeAnimationState(string animationName, bool state)
    {
        if (unitAnimator == null) return;
        unitAnimator.SetBool(animationName, state);
    }

  

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.CurrentGridPosition;

        for(int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

               
                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }
                
                if(unitGridPosition == testGridPosition)
                {
                    continue;
                }
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    continue;
                }

                validGridPositionList.Add(testGridPosition);
               
            }
        }
        

        return validGridPositionList;
    }

    public override string GetActionName()
    {
        return "Move";
    }

   
}
