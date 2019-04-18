using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGun
{
    void Shoot();
    IEnumerator Reload();
    void GiveDamage(ref RaycastHit ray, int damageAmount);
    void SpawnProjectile();
} 
