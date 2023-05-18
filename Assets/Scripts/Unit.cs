using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Unit : MonoBehaviour
{
  
    public GridPosition CurrentGridPosition { get; private set; }
    public MoveAction MoveAction { get; private set; }


    private void Awake()
    {
        MoveAction = GetComponent<MoveAction>();
       
    }

    private void Start()
    {
        CurrentGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(CurrentGridPosition, this);
    }
    void Update()
    {
        CheckGridPosition();
    }

    private void CheckGridPosition()
    {
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if(newGridPosition != CurrentGridPosition)
        {
            LevelGrid.Instance.UnitMoveGridPosition(this, CurrentGridPosition, newGridPosition);
            CurrentGridPosition= newGridPosition;
        }
    }



   
   

   
}
