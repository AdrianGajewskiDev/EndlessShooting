using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperGun : MonoBehaviour, IGun
{
    public GunAtrributes Atributes;
    Ray ray;
    RaycastHit hit;
    string tag = string.Empty;
    float timeToFireAllowed;
    [SerializeField] ParticleSystem muzzle;

    public void GiveDamage(ref RaycastHit ray, int damageAmount)
    {
        if(ray.transform != null)
        {
            if(ray.transform.GetComponent<EnemyHealth>() != null)
            {
                ray.transform.GetComponent<EnemyHealth>().GiveDamageToEnemy(damageAmount);
            }
        }
    }

    public IEnumerator Reload()
    {
        if(InputController.Reload_Button)
        {
            if(Atributes.MaxAmmo >= 1 && Atributes.Animator.GetBool("Scope") == false)
            {
                Debug.Log("Reloading");               
                Atributes.ReloadSound.Play();
                Atributes.Animator.SetBool("Reload",true);
                Atributes.Animator.SetBool("Idle",false);
                GetComponentInParent<SniperGun>().enabled = false;
                yield return new WaitForSeconds(3f);
                GetComponentInParent<SniperGun>().enabled = true;   
                Atributes.Animator.SetBool("Reload",false);
                Atributes.Animator.SetBool("Idle",true);
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
                else
                {
                    Debug.Log("No Ammo");
                }
            }
        }
    }
    void Aim()
    {
        if(InputController.Right_Mouse)
        {
            Atributes.Animator.SetBool("Scope", true);
        }
        
        if(!InputController.Right_Mouse)
        {
            Atributes.Animator.SetBool("Scope", false);
        }
    }
    public void Shoot()
    {
        if(InputController.Left_Mouse && Atributes.CurrentAmmoInCip >= 1 && Time.time >= timeToFireAllowed )
        {
            Atributes.Animator.SetBool("Shoot", true);
            Atributes.Animator.SetBool("Idle", false);
            Debug.Log("Shooting");
            Atributes.CurrentAmmoInCip -= 1;
            timeToFireAllowed = Time.time + 1 / Atributes.RateOfFire;
            Atributes.ShotSound.Play();
            SpawnProjectile();

            if(tag == "Enemy")
            {
                GiveDamage(ref hit, Atributes.Damage);
            }

            SpawnProjectile();
            SpawnHitEffect();
        }
        else
        {
            Atributes.Animator.SetBool("Shoot", false);
            Atributes.Animator.SetBool("Idle", true);  
        }    
            
    }

    void SpawnHitEffect()
    {

    }
    public void SpawnProjectile()
    {
        muzzle.Play();
    }

    void Start()
    {
        Atributes.Animator = GetComponentInParent<Animator>();
        Atributes.Crosshair.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        UIAmmoController.Type = Atributes.Type;
        Aim();
        Shoot();
        StartCoroutine(Reload());
        SetInput();
    }

    void FixedUpdate()
    {
        ray = Camera.main.ScreenPointToRay(Atributes.Crosshair.transform.position);

        if(Physics.Raycast(ray, out hit, Atributes.Range))
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

    void SetInput()
    {
        InputController.Left_Mouse = Input.GetButtonDown("Fire1");
        InputController.Right_Mouse = Input.GetMouseButton(1);
        InputController.Reload_Button = Input.GetButtonDown("Reload");
    }

}
