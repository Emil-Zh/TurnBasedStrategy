using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction
{
    [SerializeField] private float rotationSpeed = 360f;
    private float totalSpinAmout;

    




    private void Update()
    {
        if (!isActive)
        {
            return;
        }
        float spinAddAmount = rotationSpeed * Time.deltaTime;
        totalSpinAmout += spinAddAmount;

        transform.eulerAngles += new Vector3(0, spinAddAmount, 0);

        if(totalSpinAmout >= 360f)
        {
            isActive= false;
            onActionComplete();
        }
    }

    public void Spin(Action onActionComplete)
    {
        this.onActionComplete= onActionComplete;
        isActive = true;
        totalSpinAmout= 0;
        Debug.Log("Spin");
    }
}
