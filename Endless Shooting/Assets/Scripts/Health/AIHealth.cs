using UnityEngine;
using UnityEngine.AI;

public class AIHealth : MonoBehaviour, IHealth
{
    private enum AIType
    {
        Enemy,
        Friend
    }

    public AI aiScript;
    [SerializeField] AIType AiType;
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
            aiScript.enabled = false;
            agent.enabled = false;
            Destroy(gameObject,1f);
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

    void OnDestroy()
    {
        switch(AiType)
            {
                case AIType.Enemy:
                {
                    AIRespawner.enemyAmount -= 1;
                    GlobalScore.FriendScore += 1;
                }break;

                case AIType.Friend:
                {
                    AIRespawner.friendAmount -= 1;
                    GlobalScore.EnemyScore += 1;
                }break;
            }
    }
}
