using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using TMPro;

public class UnitActionSystemUI : MonoBehaviour
{
    [SerializeField] private Transform actionButtonPrefab;
    [SerializeField] private Transform actionButtonContainerTransform;
    [SerializeField] private TextMeshProUGUI actionPointsText;

    private List<ActionButtonUI> actionButtonUIList;


    private void Awake()
    {
        actionButtonUIList = new List<ActionButtonUI>();
    }
    private void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnitChange += UnitActionSystem_OnSelectedUnitChange;
        UnitActionSystem.Instance.OnSelectedActionChange += UnitActionSystem_OnSelectedActionChange;
        UnitActionSystem.Instance.OnActionStart += UnitActionSystem_OnActionStart;
        TurnSystem.Instance.OnTurnChange += TurnSystem_OnTurnChange;
        Unit.OnAnyPointsChanged += Unit_OnAnyPointsChanged;

        UpdateActionPoints();
        CreateUnitActionButtons();
        UpdateSelectedButtonVisual();
    }

    private void CreateUnitActionButtons()
    {
        DestroyButtons();
        Unit selectedUnit = UnitActionSystem.Instance.SelectedUnit;

        foreach (BaseAction baseAction in selectedUnit.GetBaseActionArray())
        {
            Transform actionButtonTranform = Instantiate(actionButtonPrefab, actionButtonContainerTransform);
            ActionButtonUI actionButtonUI = actionButtonTranform.GetComponent<ActionButtonUI>();
            actionButtonUI.SetBaseAction(baseAction);

            actionButtonUIList.Add(actionButtonUI);
        }

    }

    private void DestroyButtons()
    {
        foreach (Transform buttonTransform in actionButtonContainerTransform)
        {
            Destroy(buttonTransform.gameObject);
        }
        actionButtonUIList.Clear();
    }

    private void UnitActionSystem_OnSelectedUnitChange(object sender, EventArgs e)
    {
        CreateUnitActionButtons();
        UpdateSelectedButtonVisual();
        UpdateActionPoints();
    }
    private void UnitActionSystem_OnSelectedActionChange(object sender, EventArgs e)
    {
        UpdateSelectedButtonVisual();
    }

    private void UpdateSelectedButtonVisual()
    {

        foreach(ActionButtonUI actionButtonUI in actionButtonUIList)
        {
            actionButtonUI.UpdateSelectedVisual();
        }
    }

    private void UpdateActionPoints()
    {
        int selectedUnitActionPoints = UnitActionSystem.Instance.SelectedUnit.ActionPoints;
        actionPointsText.text = "Action points: " + selectedUnitActionPoints;
    }

    private void UnitActionSystem_OnActionStart(object sender, EventArgs e)
    {
        UpdateActionPoints();
    }
    private void TurnSystem_OnTurnChange(object sender, EventArgs e)
    {
        UpdateActionPoints();
    }
    private void Unit_OnAnyPointsChanged(object sender, EventArgs e)
    {
        UpdateActionPoints();
    }
}
