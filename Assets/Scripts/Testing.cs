using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Unit unit;
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            

            GridVisualSystem.Instance.HideAllGridPosition();
            GridVisualSystem.Instance.ShowGridPositionList(unit.MoveAction.GetValidActionGridPositionList());
        }
    }



}
