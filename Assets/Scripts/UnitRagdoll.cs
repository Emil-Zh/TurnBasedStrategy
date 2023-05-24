using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitRagdoll : MonoBehaviour
{
    [SerializeField] private Transform ragdollRootBone;

    private float explosionForce = 300f;
    private float explosionRadius = 10f;


    public void Setup(Transform originalRootBone)
    {
        MathchAllChildTransforms(originalRootBone, ragdollRootBone);

        ApplyExplosionToRagdoll(ragdollRootBone, explosionForce, transform.position, explosionRadius);
       
    }

    private void MathchAllChildTransforms(Transform root, Transform clone)
    {

        foreach (Transform child in root)
        {
            Transform cloneChild = clone.Find(child.name);
            if (cloneChild != null)
            {
                cloneChild.position = child.position;
                cloneChild.rotation = child.rotation;
                Debug.Log(child.name + child.position + child.rotation + "\n" +
                    cloneChild.name + cloneChild.position + " " + cloneChild.rotation);
                MathchAllChildTransforms(child, cloneChild);
            }
        }
    }

    private void ApplyExplosionToRagdoll(Transform root, float explosionForce, Vector3 explosionPosition, float explosionRange)
    {
        foreach (Transform child in root)
        {
            if(child.TryGetComponent<Rigidbody>(out Rigidbody childRigidbody))
            {
                childRigidbody.AddExplosionForce(explosionForce, explosionPosition, explosionRange);

                ApplyExplosionToRagdoll(child, explosionForce, explosionPosition, explosionRange);
            }
            
        }
    }
 
}
