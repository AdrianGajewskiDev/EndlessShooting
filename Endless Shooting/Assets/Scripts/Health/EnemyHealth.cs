using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour, IHealth
{
    [SerializeField] int health = 5;
    [SerializeField] Transform[] spawnPoints;
    Animator anim;

    public void GetDamage(int amount)
    {
        health -= amount;
    }

    public bool IsDead()
    {
        if(health <= 0)
            return true;
        else
            return false;
    }

    public void Respawn()
    {
        health = 15;

        int index = Random.Range(0, spawnPoints.Length);
        var spawnPoint = spawnPoints[index];
        
        GetComponent<EnemyAI>().enabled = true;
        GetComponent<EnemyAI>().target = null;
        transform.position = spawnPoint.position;
        anim.SetBool("Die", false);
        GetComponent<EnemyAI>().isDead = true;
        GetComponent<NavMeshAgent>().speed = 3.5f;
    }

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        DieAndWaitToRespawn();
    }

    async void  DieAndWaitToRespawn()
    {
        if(health < 1)
        {
            anim.SetBool("Die", true);
            var speed = GetComponent<NavMeshAgent>();
            speed.speed = 0;
            GetComponent<EnemyAI>().enabled = false;
            await Task.Delay(2000);
            Respawn();
        }
    }
}
