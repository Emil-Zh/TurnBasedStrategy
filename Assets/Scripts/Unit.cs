using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Unit : MonoBehaviour
{
  
    public GridPosition CurrentGridPosition { get; private set; }
    public MoveAction MoveAction { get; private set; }
    public SpinAction SpinAction { get; private set; }

    private BaseAction[] baseActionArray;

    private void Awake()
    {
        MoveAction = GetComponent<MoveAction>();
        SpinAction = GetComponent<SpinAction>();
        baseActionArray = GetComponents<BaseAction>();
       
    }

    private void Start()
    {
        CurrentGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(CurrentGridPosition, this);
    }
    private void Update()
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

    public BaseAction[] GetBaseActionArray() => baseActionArray;



   
   

   
}
