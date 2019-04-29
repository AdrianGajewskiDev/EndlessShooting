using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static int Health = 10;

    public void GivePlayerDamage(int amount)
    {
        Health -= amount;
    }

    void Update()
    {
        Debug.Log(Health);
    }
}
