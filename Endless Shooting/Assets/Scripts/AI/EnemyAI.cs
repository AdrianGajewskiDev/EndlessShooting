using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform[] waypoints;
    int currentWaypontIndex;
    NavMeshAgent NMAgent;

    void Start()
    {
        NMAgent = this.GetComponent<NavMeshAgent>();

        if(NMAgent == null)
            Debug.LogError("No mesh agent at" + gameObject.name);

        currentWaypontIndex = Random.Range(0, waypoints.Length);

        NMAgent.SetDestination(waypoints[currentWaypontIndex].transform.position);
    }


    void Update()
    {
        if (!NMAgent.pathPending)
        {
            if (NMAgent.remainingDistance <= NMAgent.stoppingDistance)
            {
                if (!NMAgent.hasPath || NMAgent.velocity.sqrMagnitude == 0f)
                {
                    SetDestination();
                }
            }
        }
    }
    void SetDestination()
    {
        currentWaypontIndex = Random.Range(0, waypoints.Length);
        NMAgent.SetDestination(waypoints[currentWaypontIndex].transform.position);
    }
}
