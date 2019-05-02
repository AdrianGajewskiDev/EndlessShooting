using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    public static int Health = 25;
    [SerializeField] Transform[] spawnPoints;


    public void GetDamage(int amount)
    {
        Health -= amount;    
    }

    public bool IsDead()
    {
        if(Health <= 0)
            return true;
        else
            return false;
    }

    public void Respawn()
    {
    }

    IEnumerator AddHealth()
    {
        while (true)
        { 
            if (Health < 25)
            { 
                Health += 5; 
                yield return new WaitForSeconds(1);
            } 
            else 
            { 
                yield return null;
            }
        }
    }

    void Start()
    {
        StartCoroutine(AddHealth());
    }
}
