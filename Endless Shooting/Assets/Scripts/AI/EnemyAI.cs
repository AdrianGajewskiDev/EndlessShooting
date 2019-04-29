using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform[] waypoints;
    [SerializeField] float rotationSpeed = 5f; 
    [SerializeField] ParticleSystem muzzle;
    float timeToFireAllowed = 0;
    [SerializeField] GunAtrributes Atributes;
    int currentWaypontIndex;
    NavMeshAgent NMAgent;
    bool InAttackMode;
    bool Patroling = true;
    Animator anim;
    Transform target;
    RaycastHit hit;
    Rigidbody rBody;

    void Start()
    {
        NMAgent = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponentInChildren<Animator>();
        rBody = this.GetComponent<Rigidbody>();

        if(NMAgent == null)
            Debug.LogError("No mesh agent at" + gameObject.name);

        if(Patroling)
        {
            currentWaypontIndex = Random.Range(0, waypoints.Length);
            NMAgent.SetDestination(waypoints[currentWaypontIndex].transform.position);
        }

    }


    void Update()
    {
        if (!NMAgent.pathPending && Patroling)
        {
            if (NMAgent.remainingDistance <= NMAgent.stoppingDistance)
            {
                if (!NMAgent.hasPath || NMAgent.velocity.sqrMagnitude == 0f)
                {
                    SetDestination();
                }
            }
        }
        if(Atributes.CurrentAmmoInCip < 1 )
            StartCoroutine(Reload());

        Attack();
    }
    void SetDestination()
    {
        currentWaypontIndex = Random.Range(0, waypoints.Length);
        NMAgent.SetDestination(waypoints[currentWaypontIndex].transform.position);
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            GetComponent<SphereCollider>().radius = 10;
            InAttackMode = true;
            target = col.transform;
            NMAgent.SetDestination(target.position);
        }
    }

    void OnTriggerExit(Collider col)
    {
        GetComponent<SphereCollider>().radius = 5;
        Patroling = true;
        InAttackMode = false;
        NMAgent.speed = 3.5f;
        anim.SetBool("Attacking", InAttackMode);
        target = null;
        SetDestination();

    }

    void Attack()
    {
        if(InAttackMode)
        {
            var targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            Patroling = false;
            NMAgent.speed = 0;
            InAttackMode = true;
            anim.SetBool("Attacking", InAttackMode);

        }
        
    }

    void FixedUpdate()
    {
        if(Physics.Raycast(Atributes.ShotPoint.transform.position, Atributes.ShotPoint.TransformDirection(Vector3.forward), out hit, Atributes.Range))
        {
            if(hit.transform != null)
            {
                Debug.Log("EnemyTagging " + hit.transform.tag);

                if(hit.transform.CompareTag("Player"))
                    Shoot();
            }
        }
    }

    void Shoot()
    {
        if(Atributes.CurrentAmmoInCip >= 1 && Time.time >= timeToFireAllowed)
        {
            Debug.Log("Shooting");
            Atributes.ShotSound.Play();
            Atributes.CurrentAmmoInCip -= 1;
            timeToFireAllowed = Time.time + 1 / Atributes.RateOfFire;
            SpawnMuzzle();

            if(hit.transform.GetComponent<PlayerHealth>() != null)
            {
                hit.transform.GetComponent<PlayerHealth>().GivePlayerDamage(Atributes.Damage);
            }
        }
    }

    IEnumerator Reload()
    {
        if(Atributes.MaxAmmo >= 1 )
        {
            Atributes.ReloadSound.Play();
            yield return new WaitForSeconds(3f);
            if(Atributes.MaxAmmo >= 1)
            {
                if(Atributes.CurrentAmmoInCip > 0)
                {
                    var currentAmmo = Atributes.CurrentAmmoInCip;
                    var ammoToAdd = Atributes.ClipSize - currentAmmo;
                    Atributes.MaxAmmo -= ammoToAdd;
                    Atributes.CurrentAmmoInCip += ammoToAdd;
                }
                    
                if(Atributes.CurrentAmmoInCip == 0)
                {
                    var ammoToAdd = Atributes.ClipSize;
                    Atributes.MaxAmmo -= ammoToAdd;
                    Atributes.CurrentAmmoInCip += ammoToAdd;
                }  
            }
        }
    }

    void SpawnMuzzle()
    {
        muzzle.Play();
    }


}
