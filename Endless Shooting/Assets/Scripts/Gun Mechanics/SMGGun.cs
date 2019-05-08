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
            if(ray.transform.GetComponent<AIHealth>() != null)
                ray.transform.GetComponent<AIHealth>().GetDamage(damageAmount);
        }
    }

    public IEnumerator Reload()
    {
        if(InputController.Reload_Button)
        {
            if(Atrributes.MaxAmmo >= 1 )
            {
                Atrributes.ReloadSound.Play();
                Atrributes.Animator.SetBool("Reload",true);
                GetComponentInParent<SMGGun>().enabled = false;
                yield return new WaitForSeconds(3f);
                GetComponentInParent<SMGGun>().enabled = true;   
                Atrributes.Animator.SetBool("Reload",false);
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
            }
        }
    }

    public void Shoot()
    {
        if(InputController.Left_Mouse   && Time.time >= timeToFireAllowed && Atrributes.CurrentAmmoInCip >= 1)
        {
            timeToFireAllowed = Time.time + 1 / Atrributes.RateOfFire;              
            Atrributes.ShotSound.Play();
            Atrributes.Animator.SetBool("Shoot", true);
            SpawnProjectile();
            Atrributes.CurrentAmmoInCip -= 1;
            
            if(hit.distance <= Atrributes.Range)
            {
                if(tag == "Enemy")
                {
                    GiveDamage(ref hit, Atrributes.Damage);
                    SpawnBlood();
                }
                else
                    SpawnHitEffect();
                
            }
        }
        else if(!InputController.Left_Mouse)
        {
            Atrributes.Animator.SetBool("Shoot", false);
        }
    }

    void Start()
    {
        Atrributes.Animator = GetComponentInParent<Animator>();
        Atrributes.Crosshair.SetActive(true);
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
        Destroy(GameObject.Find("SmallExplosionEffect(Clone)"),0.02f);
        Atrributes.Crosshair.SetActive(true);
        UIAmmoController.Type = Atrributes.Type;
        SetInput();
        Shoot();
        StartCoroutine(Reload());
        Scope();
    }

    void SetInput()
    {
        InputController.Left_Mouse = Input.GetMouseButton(0);
        InputController.Reload_Button = Input.GetButtonDown("Reload");
        InputController.Right_Mouse = Input.GetMouseButton(1);
    }

    void Scope()
    {
        if(InputController.Right_Mouse)
        {
            Atrributes.Animator.SetBool("Scope", true);
        }
        else
        {
            Atrributes.Animator.SetBool("Scope", false);
        }
    }

    void SpawnHitEffect()
    {
        var ets = Atrributes.VFX[0];

        if(ets != null)
        {
            if(hit.point != null)
                Instantiate(ets, hit.point, hit.transform.rotation);
        }

        Destroy(ets);
    }

    void SpawnBlood()
    {
        var ets = Atrributes.VFX[1];

        if(ets != null)
        {
            Instantiate(ets, hit.point, hit.transform.rotation);
        }

        Destroy(ets);
    }
}
