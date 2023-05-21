using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class SpinAction : BaseAction
{
    [SerializeField] private float rotationSpeed = 360f;
    private float totalSpinAmout;

    




    private void Update()
    {
        if (!isActive)
        {
            return;
        }
        float spinAddAmount = rotationSpeed * Time.deltaTime;
        totalSpinAmout += spinAddAmount;

        transform.eulerAngles += new Vector3(0, spinAddAmount, 0);

        if(totalSpinAmout >= 360f)
        {
            ActionComplete();
        }
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        ActionStart(onActionComplete);
        totalSpinAmout= 0;
        Debug.Log("Spin");
    }

    public override string GetActionName()
    {
        return "Spin";
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.CurrentGridPosition;

        validGridPositionList.Add(unitGridPosition);
        return validGridPositionList;
        

    }
    public override int GetActionPointsCost()
    {
        return 2;
    }
}
