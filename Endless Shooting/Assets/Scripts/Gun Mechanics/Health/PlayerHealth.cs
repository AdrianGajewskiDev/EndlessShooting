using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static int Health = 25;

    public void GivePlayerDamage(int amount)
    {
        Health -= amount;
    }

    IEnumerator AddHealth()
    {
        while (true)
        { 
            if (Health < 25)
            { 
                Health += 3; 
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
