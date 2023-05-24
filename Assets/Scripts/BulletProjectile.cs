using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 200f;
    [SerializeField] private Transform bulletHitVFX;

    private TrailRenderer trailRenderer;
    private Vector3 targetPosition;
    private void Awake()
    {
        trailRenderer= GetComponentInChildren<TrailRenderer>();
    }
    public void Setup(Vector3 targetPostion)
    {
        this.targetPosition = targetPostion;
    }

    private void Update()
    {
        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        float distanceBeforeMoving = Vector3.Distance(transform.position, targetPosition);

        
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        float distnaceAfterMoving = Vector3.Distance(transform.position, targetPosition);

        if(distanceBeforeMoving< distnaceAfterMoving)
        {
            transform.position = targetPosition;

            trailRenderer.transform.parent = null;
            Destroy(gameObject);

            Instantiate(bulletHitVFX,targetPosition, Quaternion.identity);
        }



    }
}
