using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDebugObject : MonoBehaviour
{
    private GridObject gridObject;

    private TMPro.TextMeshPro m_TextMeshPro;

    private void Awake()
    {
        m_TextMeshPro = GetComponentInChildren<TMPro.TextMeshPro>();
     
    }
    public void SetGridObject(GridObject gridObject)
    {
        this.gridObject = gridObject;
    }

    private void Update()
    {
        m_TextMeshPro.text = gridObject.ToString();
    }


}
