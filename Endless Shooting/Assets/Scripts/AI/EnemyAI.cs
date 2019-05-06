using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class EnemyAI : AI
{
    [Header("Field of view")]
    [SerializeField] float maxRadius;
    [SerializeField] float maxAngle;
    [SerializeField] LayerMask mask;
    [SerializeField] LayerMask raycastHitmask;

    [Header("Attacking")] 
    [SerializeField] GunAtrributes Atributes;
    [SerializeField] ParticleSystem muzzle;
    [SerializeField] bool InAttackMode = false;
    [SerializeField] float rotateSpeed;
    private float _timeToFireAllowed = 0f;
    private Transform targetAI;
    private Transform targetPlayer;

    void OnDrawGizmos()
    {
        DrawGizmos(transform, maxRadius, maxAngle );
    }

    void FixedUpdate()
    {
        targetAI = ScanForTarget<FriendAI>(transform, mask, maxRadius, maxAngle);
        targetPlayer = ScanForTarget<FirstPersonController>(transform, mask, maxRadius, maxAngle);

        if(targetAI != null)
            RotateToTarget(transform, targetAI, rotateSpeed);

        if(targetPlayer != null)
            RotateToTarget(transform, targetPlayer, rotateSpeed);

        

        Shoot();
    }

    void Update()
    {
        if(Atributes.CurrentAmmoInCip < 1)
            Reload(Atributes.MaxAmmo, Atributes.ClipSize, Atributes.CurrentAmmoInCip, Atributes.ReloadSound);
    }

    public override void Shoot()
    {
        if(Time.time >= _timeToFireAllowed  && Atributes.CurrentAmmoInCip >= 1)
        {
            Atributes.CurrentAmmoInCip -= 1;
            _timeToFireAllowed = Time.time + 1 / Atributes.RateOfFire;
            RaycastHit hitInfo;
            if(Physics.Raycast(Atributes.ShotPoint.position, Atributes.ShotPoint.TransformDirection(Vector3.forward), out hitInfo, Atributes.Range, raycastHitmask))
            {
                if(hitInfo.transform != null) 
                {   
                    if(hitInfo.transform.CompareTag("PlayerTeam") || hitInfo.transform.CompareTag("Player"))
                    {
                        Atributes.ShotSound.Play();
                        muzzle.Play();
                        GiveDamage(ref hitInfo, Atributes.Damage);
                    }   
                }
            }
        }    
    }

    public override void GiveDamage(ref RaycastHit hit, int damageAmount)
    {
        IHealth health;
        if(hit.transform.GetComponent<IHealth>() != null)
        {
            health = hit.transform.GetComponent<IHealth>();
            health.GetDamage(damageAmount);
        }
    }
}
