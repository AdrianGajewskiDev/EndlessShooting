using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIScaner : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        GetComponentInParent<EnemyAI>().target = col.transform;

        if(col.CompareTag(GetComponentInParent<EnemyAI>().tagToSearch))
        {
            GetComponent<SphereCollider>().radius = 10;
            GetComponentInParent<EnemyAI>().InAttackMode = true;

            GetComponentInParent<NavMeshAgent>().speed = 0;

            var targetRotation = Quaternion.LookRotation(col.transform.position - transform.parent.position);
            transform.parent.rotation = Quaternion.Slerp(transform.parent.rotation, targetRotation, GetComponent<EnemyAI>().rotationSpeed * Time.deltaTime);

            if(col.transform.tag == "Player")
                EnemyAI.AiEnemy = false;
            else if(col.transform.tag == "PlayerTeam")
                EnemyAI.AiEnemy = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.CompareTag(GetComponentInParent<EnemyAI>().tagToSearch))
        {
            GetComponent<SphereCollider>().radius = 5;
            GetComponentInParent<EnemyAI>().target = null;
            GetComponentInParent<EnemyAI>().InAttackMode = false;
            GetComponentInParent<NavMeshAgent>().speed = 3.5f;

        }
    }
}
