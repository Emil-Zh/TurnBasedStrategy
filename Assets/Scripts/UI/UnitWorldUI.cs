using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitWorldUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI actionPointsText;
    [SerializeField] Unit unit;
    [SerializeField] private Image healthBarImage;
    [SerializeField] private HealthSystem healthSystem;

    private void Start()
    {
        Unit.OnAnyPointsChanged += Unit_OnAnyPointsChanged;
        healthSystem.OnHealthChange += HealthSystem_OnHealthChange;

        UpdateActionPointsText();
        UpdateHealthBar();
    }

    private void Unit_OnAnyPointsChanged(object sender, EventArgs e)
    {
        UpdateActionPointsText();
    }
    private void HealthSystem_OnHealthChange(object sender, EventArgs e)
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        healthBarImage.fillAmount = healthSystem.GetNormalisedHealth();
    }
    private void UpdateActionPointsText()
    {
        actionPointsText.text = unit.ActionPoints.ToString();
    }


}
