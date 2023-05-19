using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{

    public static UnitActionSystem Instance { get; private set; }

    public event EventHandler OnSelectedUnitChange;

    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;


    private bool isBusy;
    
    

    private void Awake()
    {
        SetSingleton();
    }

    private void Update()
    {
        if (isBusy)
        {
            return;
        }
        
        if(Input.GetMouseButtonDown(0))
        {
            if (TryHandleUnitSelection()) return;

            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());

            if (selectedUnit.MoveAction.IsValidGridPosition(mouseGridPosition))
            {
                SetBusy();
                selectedUnit.MoveAction.Move(mouseGridPosition, ClearBusy);
            }
            
        }

        if (Input.GetMouseButton(1))
        {
            SetBusy();
            selectedUnit.SpinAction.Spin(ClearBusy);
        }
            
      
    }

    public Unit GetSelectedUnit
    {
        get { return selectedUnit; }
    }

    private void SetBusy()
    {
        isBusy = true;
    }

    private void ClearBusy()
    {
        isBusy = false;
    }
    private bool TryHandleUnitSelection()
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
    private void SetSingleton()
    {
        if (Instance != null)
        {

            Debug.LogError("There's more than one UnitActionSystem" + transform + " - " + Instance);
            Destroy(Instance);
            return;
        }
        Instance= this;
    }
}
