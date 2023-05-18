using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveAction : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private int maxMoveDistance = 4;
    Animator unitAnimator;
    string isWalkingAnimation = "IsWalking";
    private Vector3 targetPosition;
    private Unit unit;
    private void Awake()
    {
        unit = GetComponent<Unit>();
        unitAnimator = GetComponentInChildren<Animator>();
        targetPosition = transform.position;
    }
    private void Update()
    {
        MoveToTarget();
    }
    private void MoveToTarget()
    {
        float stoppingDisntacce = 0.01f;
        float distanceBetweenUnitAndTarget = Vector3.Distance(transform.position, targetPosition);
        if (distanceBetweenUnitAndTarget >= stoppingDisntacce)
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            Rotate(moveDirection);
            transform.position += moveDirection * Time.deltaTime * moveSpeed;

            ChangeAnimationState(isWalkingAnimation, true);
        }
        else
        {
            ChangeAnimationState(isWalkingAnimation, false);
        }

       
    }

    public void Move(GridPosition target)
    {
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

    public bool IsValidGridPosition(GridPosition gridPosition)
    {
        List<GridPosition> validGridPositionList = GetValidActionGridPositionList();
        return validGridPositionList.Contains(gridPosition);
    }

    public List<GridPosition> GetValidActionGridPositionList()
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
                Debug.Log(testGridPosition);
            }
        }
        

        return validGridPositionList;
    }
}
