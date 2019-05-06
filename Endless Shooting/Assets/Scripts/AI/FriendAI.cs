using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendAI : AI
{
    [Header("Field of view")]
    [SerializeField] float maxRadius;
    [SerializeField] float maxAngle;
    [SerializeField] LayerMask mask;

    [Header("Attacking")]
    [SerializeField] GunAtrributes Atributes;
    bool InAttackMode = false;

    void OnDrawGizmos()
    {
        DrawGizmos(transform, maxRadius, maxAngle );
    }

    void FixedUpdate()
    {
        ScanForTarget<EnemyAI>(transform, mask, maxRadius, maxAngle);
    }

    public override void Shoot()
    {
        throw new System.NotImplementedException();
    }

    public override void GiveDamage(ref RaycastHit hit, int damageAmount)
    {
        throw new System.NotImplementedException();
    }
}
