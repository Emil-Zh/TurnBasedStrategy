using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    private GridPosition gridPosition;
    private GridSystem gridSystem;
    public List<Unit> unitList { get; set; }

    public GridObject(GridPosition gridPosition, GridSystem gridSystem)
    {
        this.gridPosition = gridPosition;
        this.gridSystem = gridSystem;
        unitList= new List<Unit>();
    }

    public override string ToString()
    {
        string unitString = "";
        foreach(Unit unit in unitList)
        {
            unitString += unit + "\n";
        }
        return gridPosition.ToString() + "\n" + unitString;
    }

    public void AddUnit(Unit unit)
    {
        unitList.Add(unit);
    }

    public void RemoveUnit(Unit unit)
    {
        unitList.Remove(unit);
    }

    public List<Unit> GetUnitList(Unit unit)
    {
        return unitList;
    }

    public bool HasAnyUnit()
    {
        return unitList.Count> 0;
    }

    public Unit GetUnit()
    {
        if (!HasAnyUnit())
        {
            return null;
        }
        return unitList[0];
    }
}
