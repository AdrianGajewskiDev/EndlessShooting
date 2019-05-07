using System.Threading.Tasks;
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
    [SerializeField] Transform[]
     respawnPoints;
    private const int MAX_HEALTH = 15;
    private int _health = 15;
    private Animator anim;
    private NavMeshAgent agent;
    private bool _addPoints = false;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    public async void Die()
    {
        if(IsDead() )
        {
            anim.SetBool("Die", true);
            aiScript.enabled = false;
            agent.enabled = false;
            _addPoints = true;
            Respawn();
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
        aiScript.enabled = true;
        agent.enabled = true;
        anim.SetBool("Die", false);
        _health = MAX_HEALTH;
        var index = Random.Range(0, respawnPoints.Length);
        var respawnPoint = respawnPoints[index].position;
        transform.position = respawnPoint;

        if(_addPoints == true)
        {
            _addPoints = false;
            switch(AiType)
            {
                case AIType.Enemy:
                {
                    GlobalScore.FriendScore += 1;
                }break;

                case AIType.Friend:
                {
                    GlobalScore.EnemyScore += 1;
                }break;
            }
        }
    }
}
