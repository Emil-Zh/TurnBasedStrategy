using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public static LevelGrid Instance { get; private set; }
    GridSystem gridSystem;
    [SerializeField] private Transform gridDebugObjectPrefab;

    private void Awake()
    {
        SetSingleton();
        
        gridSystem = new GridSystem(10, 10, 2);
        gridSystem.CreateDebugObjects(gridDebugObjectPrefab);
    }
   

    public void AddUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.AddUnit(unit);

    }

    
    
    public void RemoveUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.RemoveUnit(unit);
    }

    public void UnitMoveGridPosition(Unit unit, GridPosition fromGridPosition, GridPosition toGridPosition)
    {
        RemoveUnitAtGridPosition(fromGridPosition, unit);
        AddUnitAtGridPosition(toGridPosition, unit);
    }

    public int GetGridWidth() => gridSystem.Width;
    public int GetGridHeight() => gridSystem.Height;
    

    private void SetSingleton()
    {
        if (Instance != null)
        {

            Debug.LogError("There's more than one LevelGrid" + transform + " - " + Instance);
            Destroy(Instance);
            return;
        }
        Instance = this;
    }

    public List<Unit> GetUnitListAtGridPostion(GridPosition gridPosition) => gridSystem.GetGridObject(gridPosition).unitList;
    public GridPosition GetGridPosition(Vector3 worldPosition) => gridSystem.GetGridPosition(worldPosition);
    public Vector3 GetWorldPosition(GridPosition gridPosition) => gridSystem.GetWorldPosition(gridPosition);

    public bool IsValidGridPosition(GridPosition gridPosition) => gridSystem.IsValidGridPosition(gridPosition);
 

    public bool HasAnyUnitOnGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        if(gridObject!= null)
        {
            return gridObject.HasAnyUnit();
        }
        return false;
    }

    public Unit GetUnitAtGridPosition(GridPosition gridPosition)
    {
        return gridSystem.GetGridObject(gridPosition).GetUnit();
    }
    

}
