using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    private const int MAX_HEALTH = 25;
    public static int Health = 25;
    [SerializeField]Transform[] respawnPoints;

    public void Die() {}

    private void Update()
    {
        Respawn();
        Debug.Log(Health);
    }

    public void GetDamage(int amount)
    {
        Health -= amount;
    }

    public bool IsDead()
    {
        return Health < 1;
    }

    public void Respawn() 
    {
        if(IsDead())  
        {
            gameObject.SetActive(false);
            Health = MAX_HEALTH;
            var index = Random.Range(0, respawnPoints.Length);
            var respawnPoint = respawnPoints[index].position;
            transform.position = respawnPoint;
            gameObject.SetActive(true);
        }
    }

}
