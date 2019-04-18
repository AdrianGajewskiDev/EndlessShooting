using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    int health = 5;

    void GetDamage(int amount)
    {
        health -= amount;
    }

    void Update()
    {
        if(health < 1)
        {
            Destroy(gameObject, 0.5f);
        }
    }
}
