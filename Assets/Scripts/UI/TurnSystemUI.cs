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
    [SerializeField] private GameObject enemyTurnVisualObject;

    private void Start()
    {
        
        TurnSystem.Instance.OnTurnChange += TurnSystem_OnTurnChange;
        AddListenerForEndTurn();
        UpdateTurnNumberText();
        UpdateEnemyTurnVisual();
        UpdateEndTurnButtonVisibilty();
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
        UpdateEnemyTurnVisual();
        UpdateEndTurnButtonVisibilty();
    }

    private void UpdateEnemyTurnVisual()
    {
        enemyTurnVisualObject.SetActive(!TurnSystem.Instance.IsPlayerTurn);
    }
    private void UpdateEndTurnButtonVisibilty()
    {
        endTurnButton.gameObject.SetActive(TurnSystem.Instance.IsPlayerTurn);
    }
}
