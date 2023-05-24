using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitAnimator : MonoBehaviour
{
    [SerializeField] private Transform bulletProjectilePrefab;
    [SerializeField] private Transform shootPointTransform;
    private Animator unitAnimator;

    private string walkingAnimationName = "IsWalking";
    private string shootingAnimatonName = "Shoot";

    private void Awake()
    {
        unitAnimator= GetComponentInChildren<Animator>();

        if(TryGetComponent<MoveAction>(out MoveAction moveAction))
        {
            moveAction.OnStartMoving += MoveAction_OnStartMoving;
            moveAction.OnStopMoving+= MoveAction_OnStopMoving;
        }
        if (TryGetComponent<ShootAction>(out ShootAction shootAction))
        {
            shootAction.OnShoot += ShootAction_OnShoot;
        }
    }


    private void MoveAction_OnStartMoving(object sender, EventArgs e)
    {
       
        unitAnimator.SetBool(walkingAnimationName, true);
    }

    private void MoveAction_OnStopMoving(object sender, EventArgs e)
    {
       
        unitAnimator.SetBool(walkingAnimationName, false);
    }
    private void ShootAction_OnShoot(object sender, ShootAction.OnShootEventArgs e)
    {
        unitAnimator.SetTrigger(shootingAnimatonName);

        Transform bulletProjectileTransform = Instantiate(bulletProjectilePrefab, shootPointTransform.position, Quaternion.identity);
        BulletProjectile bulletProjectile = bulletProjectileTransform.GetComponent<BulletProjectile>();

        Vector3 targetUnitShootAtPosition = e.targetUnit.transform.position;

        targetUnitShootAtPosition.y = shootPointTransform.position.y;

        bulletProjectile.Setup(targetUnitShootAtPosition);
    }
}
