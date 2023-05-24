using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurnToCamera : MonoBehaviour
{
    [SerializeField] private bool invert;
    private Transform cameraTransform;

    private void Awake()
    {
        cameraTransform = Camera.main.transform;
    }


    private void LateUpdate()
    {
        if(invert)
        {
            Vector3 directonToCamera =(cameraTransform.position - transform.position).normalized;
            transform.LookAt(transform.position + directonToCamera * -1);
        }
        else
        {
            transform.LookAt(cameraTransform);
        }

    }
    
}
