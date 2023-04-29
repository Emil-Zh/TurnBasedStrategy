using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectedVisual : MonoBehaviour
{
    [SerializeField] private Unit unit;



    private MeshRenderer meshRenderer;
    private void Awake()
    {
        meshRenderer= GetComponent<MeshRenderer>();
        unit = GetComponentInParent<Unit>(); 
    }
    

    private void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnitChange += UnitActionSystem_OnSelectedUnitChange;

        UpdateVisual();

    }

    private void UpdateVisual()
    {
        if (unit != null && unit == UnitActionSystem.Instance.GetSelectedUnit)
        {
            meshRenderer.enabled = true;
        }
        else
        {
            meshRenderer.enabled = false;
        }
    }

    private void UnitActionSystem_OnSelectedUnitChange(object sender, EventArgs empty)
    {
        UpdateVisual();
    }
}
