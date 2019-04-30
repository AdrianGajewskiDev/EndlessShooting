using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform[] waypoints;
    public float rotationSpeed = 5f; 
    [SerializeField] ParticleSystem muzzle;
    float timeToFireAllowed = 0;
    [SerializeField] GunAtrributes Atributes;
    int currentWaypontIndex;
    NavMeshAgent NMAgent;
    public bool InAttackMode;
    bool Patroling = true;
    public static bool AiEnemy;
    Animator anim;
    public Transform target;
    RaycastHit hit;
    Rigidbody rBody;
    public bool EnemySoldier;
    public string tagToSearch;
    string playerTag = "Player";

    void Start()
    {
        NMAgent = this.GetComponentInParent<NavMeshAgent>();
        anim = this.GetComponentInChildren<Animator>();
        rBody = this.GetComponentInParent<Rigidbody>();

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
        
        if(InAttackMode == false)
            SetDestination();

        CheckForTarget();
        Debug.Log(gameObject.name + " in attack mode: " + InAttackMode);
        Attack();
    }
    void SetDestination()
    {
        if(NMAgent.hasPath == false)
        {
            currentWaypontIndex = Random.Range(0, waypoints.Length);
            NMAgent.SetDestination(waypoints[currentWaypontIndex].transform.position);
        }
        
    }

    void CheckForTarget()
    {
        if(InAttackMode == true)
        {
            NMAgent.SetDestination(target.position);
        }
        else if(InAttackMode == false)
        {
            Patroling = true;
            NMAgent.speed = 3.5f;
            anim.SetBool("Attacking", InAttackMode);
            SetDestination();
        }
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

                if(EnemySoldier == true)
                {
                    if(hit.transform.CompareTag("Player") || hit.transform.CompareTag(tagToSearch))
                    {
                        InAttackMode = true;
                        target = hit.transform;
                        Shoot(true);
                    }
                }
                else if(EnemySoldier == false)
                {
                    if(hit.transform.CompareTag(tagToSearch))
                    {
                        InAttackMode = true;
                        target = hit.transform;
                        Shoot(false);
                    }
                }
                    
            }
        }

        InAttackMode = false;
    }

    void Shoot(bool isenemy)
    {
        if(Atributes.CurrentAmmoInCip >= 1 && Time.time >= timeToFireAllowed)
        {
            Debug.Log("Shooting");
            Atributes.ShotSound.Play();
            Atributes.CurrentAmmoInCip -= 1;
            timeToFireAllowed = Time.time + 1 / Atributes.RateOfFire;
            SpawnMuzzle();

            if(isenemy == true)
            {
                if(AiEnemy == true && hit.transform.GetComponent<PlayerTeamSoldierHealth>() != null)
                {
                    hit.transform.GetComponent<PlayerTeamSoldierHealth>().GiveDamageToEnemy(Atributes.Damage);                    
                }
                
                if(AiEnemy == false && hit.transform.GetComponent<PlayerHealth>() != null)
                {
                    hit.transform.GetComponent<PlayerHealth>().GivePlayerDamage(Atributes.Damage);
                }
            }

            else if(isenemy == false)
            {
                if(hit.transform.GetComponent<EnemyHealth>() != null)
                {
                    hit.transform.GetComponent<EnemyHealth>().GiveDamageToEnemy(Atributes.Damage);
                } 
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
