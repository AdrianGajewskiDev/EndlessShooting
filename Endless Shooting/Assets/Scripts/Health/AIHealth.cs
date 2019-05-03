using UnityEngine;
using UnityEngine.AI;

public class AIHealth : MonoBehaviour, IHealth
{
    private const int MAX_HEALTH = 15;
    private int _health = 15;
    private Animator anim;
    private NavMeshAgent agent;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void Die()
    {
        if(IsDead() )
        {
            anim.SetBool("Die", true);
            agent.speed = 0;
        }
    }

    void Update()
    {
        Die();
    }

    public void GetDamage(int amount)
    {
        _health -= amount;
    }

    public bool IsDead()
    {
        return _health < 1;
    }

    public void Respawn()
    {
    }
}
