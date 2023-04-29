using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{

    public static UnitActionSystem Instance { get; private set; }

    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;

    public event EventHandler OnSelectedUnitChange;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("There's more htank one UnitActionSystem" + transform + " - " + Instance);
            Destroy(Instance);
            return;
        }
         Instance = this;
    }

    private void Update()
    {
        
        if(Input.GetMouseButtonDown(0))
        {
            if (TryHandelUnitSelection()) return;
            selectedUnit.Move(MouseWorld.GetPosition());
        }
            
        
    }


    public Unit GetSelectedUnit
    {
        get { return selectedUnit; }
    }
    private bool TryHandelUnitSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out Unit unit)) 
            {
                SetSelectedUnit(unit);
                
                return true; 
            }
        }
        return false;
    }

    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit= unit;

        OnSelectedUnitChange?.Invoke(this, EventArgs.Empty);
       
    }
}
