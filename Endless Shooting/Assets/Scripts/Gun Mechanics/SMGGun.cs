using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMGGun : MonoBehaviour, IGun
{
    public GunAtrributes Atrributes;
    public ParticleSystem muzzle;
    RaycastHit hit;
    Ray ray;
    float timeToFireAllowed = 0;

    string tag;

    public void GiveDamage(ref RaycastHit ray, int damageAmount)
    {
        if(ray.transform != null)
        {
            if(ray.transform.GetComponent<EnemyHealth>() != null)
                ray.transform.GetComponent<EnemyHealth>().GiveDamageToEnemy(damageAmount);
        }
    }

    public IEnumerator Reload()
    {
        if(InputController.Right_Mouse)
        {
            if(Atrributes.MaxAmmo >= 1 )
            {
                Debug.Log("Reloading");
                Atrributes.Animation.Play("SmgGunReload");
                Atrributes.ReloadSound.Play();
                GetComponentInParent<SMGGun>().enabled = false;
                yield return new WaitForSeconds(3f);
                GetComponentInParent<SMGGun>().enabled = true;

                if(Atrributes.MaxAmmo >= 1)
                {
                    if(Atrributes.CurrentAmmoInCip > 0)
                    {
                        var currentAmmo = Atrributes.CurrentAmmoInCip;
                        var ammoToAdd = Atrributes.ClipSize - currentAmmo;
                        Atrributes.MaxAmmo -= ammoToAdd;
                        Atrributes.CurrentAmmoInCip += ammoToAdd;
                    }
                    
                    if(Atrributes.CurrentAmmoInCip == 0)
                    {
                        var ammoToAdd = Atrributes.ClipSize;
                        Atrributes.MaxAmmo -= ammoToAdd;
                        Atrributes.CurrentAmmoInCip += ammoToAdd;
                    }  
                }
                else
                {
                    Debug.Log("No Ammo");
                }
            }
        }
    }

    public void Shoot()
    {
        if(InputController.Left_Mouse   && Time.time >= timeToFireAllowed && Atrributes.CurrentAmmoInCip >= 1)
        {
            timeToFireAllowed = Time.time + 1 / Atrributes.RateOfFire;
            Atrributes.Animation.Play("SmgGunShoot");
            Atrributes.ShotSound.Play();
            SpawnProjectile();
            Atrributes.CurrentAmmoInCip -= 1;
            
            if(hit.distance <= Atrributes.Range)
            {
                if(tag == "Enemy")
                    GiveDamage(ref hit, Atrributes.Damage);
            }
        }
    }

    void Start()
    {
        Atrributes.Animation = GetComponentInParent<Animation>();
    }
    
    public void SpawnProjectile()
    {
        muzzle.Play();
    }
    
    void FixedUpdate()
    {
        ray = Camera.main.ScreenPointToRay(Atrributes.Crosshair.transform.position);

        if(Physics.Raycast(ray, out hit, Atrributes.Range))
        { 
            Debug.Log(hit.transform.tag);      
        }

        if(hit.transform != null)
        {
            if(hit.transform.tag != null)
            {
                tag = hit.transform.tag;
            }
        }     
    }

    void Update()
    {
        UIAmmoController.Type = Atrributes.Type;
        SetInput();
        Shoot();
        StartCoroutine(Reload());
    }

    void SetInput()
    {
        InputController.Left_Mouse = Input.GetMouseButton(0);
        InputController.Right_Mouse = Input.GetButtonDown("Reload");
    }
}
