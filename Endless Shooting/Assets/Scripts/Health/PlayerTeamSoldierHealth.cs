using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class PlayerTeamSoldierHealth : MonoBehaviour, IHealth
{
    [SerializeField] int health = 15;
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
            return false;    }

    public void Respawn()
    {
        int index = Random.Range(0, spawnPoints.Length);
        var spawnPoint = spawnPoints[index];
        anim.SetBool("Die", false);
        transform.position = spawnPoint.position;
        GetComponent<EnemyAI>().enabled = true;
        GetComponent<EnemyAI>().target = null;
        var speed = GetComponent<NavMeshAgent>();
        speed.speed = 3.5f;
        GlobalScore.EnemyScore += 1;
        health = 15;
    }

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

     void Update()
    {
        DieAndWaitToRespawn();
    }

    async void DieAndWaitToRespawn()
    {
        if(health < 1)
        {
            anim.SetBool("Die", true);
            var speed = GetComponent<NavMeshAgent>();
            speed.speed = 0;
            GetComponent<EnemyAI>().enabled = false;
            Respawn();
        }
    }
}
