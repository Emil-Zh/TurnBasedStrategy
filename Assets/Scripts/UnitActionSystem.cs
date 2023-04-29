using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    [SerializeField] private Unit selectedUnit;

    [SerializeField] private LayerMask unitLayerMask;

    private void Update()
    {
        
        if(Input.GetMouseButtonDown(0))
        {
            if (TryHandelUnitSelection()) return;
            selectedUnit.Move(MouseWorld.GetPosition());
        }
            
        
    }



    private bool TryHandelUnitSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayerMask))
        {
            raycastHit.transform.TryGetComponent(out Unit unit);
            selectedUnit= unit;
            return true;
        }
        return false;
    }
}
