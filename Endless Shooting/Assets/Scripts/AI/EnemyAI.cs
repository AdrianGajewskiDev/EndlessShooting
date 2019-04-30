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
    public bool isDead = false;
    bool Patroling = true;
    Animator anim;
    public Transform target;
    RaycastHit hit;
    Rigidbody rBody;
    public bool EnemySoldier;
    public string tagToSearch;
    string playerTag = "Player";
    float distance;

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
        
        Debug.Log(InAttackMode);

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
        else if(InAttackMode == false)
        {
            Patroling = true;
            NMAgent.speed = 3.5f;
            anim.SetBool("Attacking", InAttackMode);
        }
        
    }

    void FixedUpdate()
    {
        if(Physics.Raycast(Atributes.ShotPoint.transform.position, Atributes.ShotPoint.TransformDirection(Vector3.forward), out hit, Atributes.Range))
        {
            Transform targetToShot = null;

            if(hit.transform != null)
            {
                if(EnemySoldier == true)
                {
                    if(hit.transform.CompareTag(playerTag) || hit.transform.CompareTag(tagToSearch))
                    {
                        targetToShot = hit.transform;
                        distance = Vector3.Distance(transform.position, targetToShot.position);
                        if(distance >= 20)
                        {
                            InAttackMode = false;
                            targetToShot = null;
                            distance = 0;
                            return;
                        }
                        else
                        {
                            InAttackMode = true;
                            target = hit.transform;
                            Shoot(EnemySoldier);
                        }
                    }
                }
                else if(EnemySoldier == false)
                {
                    if(hit.transform.CompareTag(tagToSearch))
                    {
                        targetToShot = hit.transform;
                        distance = Vector3.Distance(transform.position, targetToShot.position);
                        if(distance >= 20)
                        {
                            InAttackMode = false;
                            distance = 0;
                            targetToShot = null;
                            return;
                        }
                        else
                        {
                            InAttackMode = true;
                            target = hit.transform;
                            distance = Vector3.Distance(transform.position, hit.transform.position);
                            Shoot(false);
                        }
                    }
                }        
            }
        }

        Debug.Log(distance);


        if(target != null && distance >= 20)
        {
            Debug.Log(Vector3.Distance(transform.position, target.position));
            InAttackMode = false;
            target = null;
        }

    }

    void Shoot(bool isenemy)
    {
        if(Atributes.CurrentAmmoInCip >= 1 && Time.time >= timeToFireAllowed)
        {
            IHealth health = null;
            
            switch(isenemy)
            {
                case true:
                {
                    if(hit.transform.GetComponent<PlayerHealth>() == null)
                        health = hit.transform.GetComponent<PlayerTeamSoldierHealth>();
                    else
                        health = hit.transform.GetComponent<PlayerHealth>();
                }break;

                case false:
                {
                    health = hit.transform.GetComponent<EnemyHealth>();
                }break;
            }

            if(health.IsDead() == true)
            {
                InAttackMode = false;
                return;
            }

            health.GetDamage(Atributes.Damage);
            Debug.Log("Shooting");
            Atributes.ShotSound.Play();
            Atributes.CurrentAmmoInCip -= 1;
            timeToFireAllowed = Time.time + 1 / Atributes.RateOfFire;
            SpawnMuzzle();

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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == gameObject.tag)
        {
            Physics.IgnoreCollision(gameObject.GetComponent<BoxCollider>(), collision.gameObject.GetComponent<BoxCollider>());
        }
    }

}
