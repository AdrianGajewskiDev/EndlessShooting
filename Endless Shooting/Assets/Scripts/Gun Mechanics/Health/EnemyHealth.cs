using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int health = 5;

    public void GiveDamageToEnemy(int amount)
    {
        health -= amount;
    }

    void Update()
    {
        if(health < 1)
        {
            Destroy(gameObject, 0.5f);
        }
        Debug.Log(health);
    }
}
