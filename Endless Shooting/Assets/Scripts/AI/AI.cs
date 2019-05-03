using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AI : MonoBehaviour
{
    
    //NOTE: Call this function inside a OnDrawGizmos function
    //This can be used to visualize field of view
    public void DrawGizmos(Transform center, float radius, float angle)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(center.position, radius);

        Vector3 firstLine = Quaternion.AngleAxis(angle, center.up) * center.forward * radius;
        Vector3 secondLine = Quaternion.AngleAxis(-angle, center.up) * center.forward * radius;

        Gizmos.color = Color.blue;

        Gizmos.DrawRay(center.position, firstLine);
        Gizmos.DrawRay(center.position, secondLine);
    }
    
    //NOTE: Call this inside Update or FixedUpdate function
    public void ScanForTarget<T> (Transform center, LayerMask l_mask, float maxRadius, float maxAngle)
    {
        Collider[] potentialTargets = Physics.OverlapSphere(center.position, maxRadius);

        foreach(Collider col in potentialTargets)
        {
            
            if(col.transform.GetComponent<T>() != null)
            {
                var target = col.transform;

                if(target != null && IsInLineOfSight(target, center, maxAngle, maxRadius, l_mask))
                {
                    center.rotation = RotateToTarget(center, target, 5f);
                }
            }
        }
    }

    private bool IsInLineOfSight(Transform target, Transform checkingObject, float maxAngle, float range, LayerMask mask)
    {
        Vector3 direction = (target.position - checkingObject.position).normalized;

        float distance = Vector3.Distance(checkingObject.position, target.position);
        if(Vector3.Angle(checkingObject.forward, direction) <= maxAngle)
        {
            if(Physics.Raycast(checkingObject.position, direction.normalized, distance, mask))
                return false;
            
            return true;
        }

        return false;
    }

    public Quaternion RotateToTarget(Transform obj, Transform target, float speed)
    {   
        var targetRotation = Quaternion.LookRotation(target.transform.position - obj.position);

        return Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
    }
}
