using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float rotationSpeed = 10f;

    Animator unitAnimator;
    string isWalkingAnimation = "IsWalking";

    private Vector3 targetPosition;
    private GridPosition currentGridPosition;


    private void Awake()
    {
        unitAnimator = GetComponentInChildren<Animator>();
        targetPosition= transform.position;
    }

    private void Start()
    {
        currentGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(currentGridPosition, this);
    }
    void Update()
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

        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if(newGridPosition != currentGridPosition)
        {
            LevelGrid.Instance.UnitMoveGridPosition(this, currentGridPosition, newGridPosition);
            currentGridPosition= newGridPosition;
        }
    }

    public void Move(Vector3 target)
    {
        targetPosition = target;
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
}
