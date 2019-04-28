using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int health = 5;
    Animator anim;

    public void GiveDamageToEnemy(int amount)
    {
        health -= amount;
    }

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if(health < 1)
        {
            anim.SetBool("Die", true);
            var speed = GetComponent<NavMeshAgent>();
            speed.speed = 0;
            Destroy(gameObject, 5);
        }
        Debug.Log(health);
    }
}
