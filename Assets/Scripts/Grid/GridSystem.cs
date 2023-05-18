using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GridSystem  
{
    
    private int width;
    private int height;
    private float cellSize;
    private GridObject[,] gridObjectArray;

   

    public GridSystem(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        gridObjectArray = new GridObject[width, height];

        for (int x =0; x < width; x++)
        {
            for (int z =0; z<height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                gridObjectArray[x, z] = new GridObject(gridPosition, this);
            }
        }
    }


    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return new Vector3(gridPosition.x,0, gridPosition.z) * cellSize;
    }

    public GridPosition GetGridPosition(Vector3 worldPostion)
    {
        return new GridPosition(
            Mathf.RoundToInt(worldPostion.x / cellSize),
            Mathf.RoundToInt(worldPostion.z / cellSize)
            );
    }

    public void CreateDebugObjects(Transform debugPrefab)
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x,z);
                Transform debugTransform = GameObject.Instantiate(debugPrefab, GetWorldPosition(gridPosition), Quaternion.identity);
                GridDebugObject gridDebugObject = debugTransform.GetComponent<GridDebugObject>();
                gridDebugObject.SetGridObject(GetGridObject(gridPosition));
               
            }
        }
    }

    public GridObject GetGridObject(GridPosition gridPosition)
    {
        if (gridPosition.x < width && gridPosition.z < height)
        {
            return gridObjectArray[gridPosition.x, gridPosition.z];
        }
        return null;
    }

    public bool IsValidGridPosition(GridPosition gridPosition)
    {

        return  gridPosition.x >= 0 && 
                gridPosition.z >= 0 && 
                gridPosition.x < width && 
                gridPosition.z < height;
        
    }
}
