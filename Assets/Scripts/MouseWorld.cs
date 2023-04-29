using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;

    private static MouseWorld instance;

    

    private void Awake()
    {
        instance = this;
    }

    public static Vector3 GetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, instance.layerMask);
        return hitInfo.point;
    }
}
