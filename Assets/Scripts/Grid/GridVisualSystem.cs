using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridVisualSystem : MonoBehaviour
{
    [SerializeField] private Transform gridSystemVisualSinglePrefab;
    private GridSystemVisualSingle[,] gridSystemVisualSingleArray;
    public static GridVisualSystem Instance;

    private void Awake()
    {
        SetSingleton();
    }
    private void Start()
    {

        gridSystemVisualSingleArray = new GridSystemVisualSingle[LevelGrid.Instance.GetGridWidth(), LevelGrid.Instance.GetGridHeight()];
        for(int x=0; x< LevelGrid.Instance.GetGridWidth(); x++)
        {
            for(int z=0; z < LevelGrid.Instance.GetGridHeight(); z++)
            {
                GridPosition gridVisualPosition = new GridPosition(x,z);
                Transform gridVisualSingleTransform = Instantiate(gridSystemVisualSinglePrefab);
                gridVisualSingleTransform.position = LevelGrid.Instance.GetWorldPosition(gridVisualPosition);

                gridSystemVisualSingleArray[x, z] = gridVisualSingleTransform.GetComponent<GridSystemVisualSingle>();
            }
        }
    }

    private void Update()
    {
        UpdateGridVisual();
    }

    public void HideAllGridPosition()
    {
        for(int x =0; x < LevelGrid.Instance.GetGridWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetGridHeight(); z++)
            {
                gridSystemVisualSingleArray[x,z].Hide();
            }
        }
    }

    public void ShowGridPositionList(List<GridPosition> gridPositionList)
    {
        foreach(GridPosition gridPosition in gridPositionList)
        {
            gridSystemVisualSingleArray[gridPosition.x, gridPosition.z].Show();
        }
    }

    private void SetSingleton()
    {
        if (Instance != null)
        {

            Debug.LogError("There's more than one UnitActionSystem" + transform + " - " + Instance);
            Destroy(Instance);
            return;
        }
        Instance = this;
    }

    private void UpdateGridVisual()
    {
        HideAllGridPosition();

        BaseAction selectedAction = UnitActionSystem.Instance.SelectedAction;
        ShowGridPositionList(selectedAction.GetValidActionGridPositionList());
    }
}
