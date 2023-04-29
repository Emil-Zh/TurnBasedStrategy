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
   

    private void Awake()
    {
        unitAnimator = GetComponentInChildren<Animator>();
    }



    private Vector3 targetPosition;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Move(MouseWorld.GetPosition());
        }
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        float stoppingDisntacce = 0.01f;
        float distanceBetweenUnitAndTarget = Vector3.Distance(transform.position, targetPosition);
        if (distanceBetweenUnitAndTarget >= stoppingDisntacce)
        {
            ChangeAnimationState(isWalkingAnimation, true);
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            Rotate(moveDirection);
            transform.position += moveDirection * Time.deltaTime * moveSpeed;
        }
        else
        {
            ChangeAnimationState(isWalkingAnimation, false);
        }
    }

    private void Move(Vector3 target)
    {
        this.targetPosition = target;
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
