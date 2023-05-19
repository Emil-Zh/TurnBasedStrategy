using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class TurnSystemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI turnNumberText;
    [SerializeField] private Button endTurnButton;

    private void Start()
    {
        
        TurnSystem.Instance.OnTurnChange += TurnSystem_OnTurnChange;
        AddListenerForEndTurn();
        UpdateTurnNumberText();
    }

    private void AddListenerForEndTurn()
    {
        endTurnButton.onClick.AddListener(()=>TurnSystem.Instance.NextTurn());
    }

    private void UpdateTurnNumberText()
    {
        turnNumberText.text = "TURN " + TurnSystem.Instance.TurnNumber.ToString();
    }
   
    private void TurnSystem_OnTurnChange(object sender, EventArgs e)
    {
        UpdateTurnNumberText();
    }
}
