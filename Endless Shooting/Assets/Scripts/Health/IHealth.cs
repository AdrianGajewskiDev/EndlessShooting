using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    bool IsDead();
    void GetDamage(int amount);
    void Respawn();
}
