using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ShootAction : BaseAction
{
    [SerializeField] private int maxShootDistance = 5;
    [SerializeField] private float aimTime = 0.2f;
    [SerializeField] private float shootTime = 0.1f;
    [SerializeField] private float cooloffTIme = 0.2f;

    private float stateTimer;
    private Unit targetUnit;
    private bool canShootBullet;
    private ShootState state;
    private float rotationSpeed = 20f;

    public event EventHandler<OnShootEventArgs> OnShoot;

    public class OnShootEventArgs : EventArgs
    {
        public Unit targetUnit;
        public Unit shootingUnit;
    }

    enum ShootState
    {
        Aiming,
        Shooting,
        Cooloff
    }
    private void Update()
    {
        if (!isActive)
        {
            return;
        }
        stateTimer -=Time.deltaTime;


        switch (state)
        {
            case ShootState.Aiming:
                RotateTowardsTarget(targetUnit);
                break;
            case ShootState.Shooting:
                if (canShootBullet)
                {
                    OnShoot?.Invoke(this, new OnShootEventArgs
                    {
                        targetUnit = targetUnit,
                        shootingUnit = unit
                    }); ;
                    Shoot();
                    canShootBullet = false;
                }
                break;
            case ShootState.Cooloff:
                break;

        }

        if (stateTimer <=0f)
        {
            NextState();
        }
    }
    public override string GetActionName()
    {
        return "Shoot";
    }
    private void NextState()
    {
        switch (state)
        {
            case ShootState.Aiming:
                stateTimer = shootTime;
                state = ShootState.Shooting;
                break;
            case ShootState.Shooting:
                if(canShootBullet)
                {
                    Shoot();
                    canShootBullet = false;
                }
                stateTimer = cooloffTIme;
                state = ShootState.Cooloff;
                break;
            case ShootState.Cooloff:
                ActionComplete();
                break;

        }
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.CurrentGridPosition;

        for (int x = -maxShootDistance; x <= maxShootDistance; x++)
        {
            for (int z = -maxShootDistance; z <= maxShootDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;
                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }
                
                int testDistance = Math.Abs(x)+ Math.Abs(z);
                if(testDistance > maxShootDistance)
                {
                    continue;
                }
                
                if (!LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    continue;
                }
                Unit targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(testGridPosition);

                if (unit.IsEnemy == targetUnit.IsEnemy)
                {
                    // Both Units on same team
                    continue;
                }

                validGridPositionList.Add(testGridPosition);

            }
        }


        return validGridPositionList;
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        ActionStart(onActionComplete);

        targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(gridPosition);

        state = ShootState.Aiming;
        stateTimer = aimTime;

        canShootBullet = true;
    }

    private void RotateTowardsTarget(Unit target)
    {
         Vector3 aimDirection = (target.transform.position - unit.transform.position).normalized;
         unit.transform.forward = Vector3.Slerp(transform.forward, aimDirection, Time.deltaTime * rotationSpeed);
    }

    private void Shoot()
    {
        targetUnit.TakeDamage(40);
    }
    


}
