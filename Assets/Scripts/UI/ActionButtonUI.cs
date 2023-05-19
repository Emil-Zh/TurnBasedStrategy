using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Button button;
    [SerializeField] private GameObject selectedGameObject;

    private BaseAction baseAction;

    private void Start()
    {
        UpdateSelectedVisual();
    }
    public void SetBaseAction(BaseAction baseAction)
    {
        this.baseAction = baseAction; 
        textMeshPro.text= baseAction.GetActionName().ToUpper();
        
        button.onClick.AddListener(() =>
        {
            UnitActionSystem.Instance.SetSelectedAction(baseAction);
        });
    }

    

    public void UpdateSelectedVisual()
    {
        BaseAction selectedBaseAction = UnitActionSystem.Instance.SelectedAction;
        selectedGameObject.SetActive(selectedBaseAction == baseAction);
    }
    
}
