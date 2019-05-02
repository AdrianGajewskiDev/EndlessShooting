using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

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
        var index = Random.Range(0, spawnPoints.Length);
        var spawnPoint = spawnPoints[index];
        Health = 25;
        gameObject.SetActive(false);
        transform.position = spawnPoint.position;
        gameObject.SetActive(true);       
    
  
        
    }

    void Update()
    {
        if(Health < 1 )
        {
            GlobalScore.EnemyScore += 1;
            Respawn();
        }
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
