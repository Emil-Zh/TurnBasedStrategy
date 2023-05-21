using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurnSystem : MonoBehaviour
{
    private int turnNumber = 1;
    private bool isPlayerTurn = true;
    public bool IsPlayerTurn { get { return isPlayerTurn; } }

    public int TurnNumber { get { return turnNumber; } }
    public static TurnSystem Instance { get; private set; }
    public event EventHandler OnTurnChange;
    private void Awake()
    {
        SetSingleton();
    }

    public void NextTurn()
    {
        turnNumber++;
        isPlayerTurn= !isPlayerTurn;
        OnTurnChange(this, EventArgs.Empty);
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

    
}
