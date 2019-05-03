using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    private const int MAX_HEALTH = 25;
    public static int Health = 25;

    public void Die() {}

    public void GetDamage(int amount)
    {
        Health -= amount;
    }

    public bool IsDead()
    {
        return Health < 1;
    }

    public void Respawn() {}
}
