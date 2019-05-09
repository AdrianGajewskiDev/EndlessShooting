using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerHealth : MonoBehaviour, IHealth
{
    private const int MAX_HEALTH = 25;
    public static int Health = 25;
    [SerializeField]Transform[] respawnPoints;
    [SerializeField]Transform player;

    public void Die() {}

    void Start()
    {
        StartCoroutine(Recover());
    }
    private void Update()
    {
        Respawn();
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
            GlobalScore.EnemyScore += 1;
            GetComponent<FirstPersonController>().enabled = false;
            GetComponent<CharacterController>().enabled = false;
            Health = MAX_HEALTH;
            var index = Random.Range(0, respawnPoints.Length);
            var respawnPoint = respawnPoints[index].position;
            player.position = respawnPoint;
            GetComponent<FirstPersonController>().enabled = true;
            GetComponent<CharacterController>().enabled = true;
        }
    }

    IEnumerator Recover()
    {
        while(true)
        {
            if(Health < MAX_HEALTH)
            {
                Health += 5;
                yield return new WaitForSeconds(2f);
            }
            else
                yield return null;
        }
        
    }

}
