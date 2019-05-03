using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendAI : AI
{
    [Header("Field of view")]
    [SerializeField] float maxRadius;
    [SerializeField] float maxAngle;
    [SerializeField] LayerMask mask;

    void OnDrawGizmos()
    {
        DrawGizmos(transform, maxRadius, maxAngle );
    }

    void FixedUpdate()
    {
        ScanForTarget<EnemyAI>(transform, mask, maxRadius, maxAngle);
    }
  
}
