using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGun : MonoBehaviour, IGun
{
    public GunAtrributes Atrributes;
    float timeToFireAllowed;
    GameObject effectToSpawn;
    RaycastHit hit;
    string tag = string.Empty;

    public void GiveDamage(ref RaycastHit ray, int damageAmount)
    {
        ray.transform.GetComponent<EnemyHealth>().GiveDamageToEnemy(damageAmount);
    }

    public IEnumerator Reload()
    {
        if(InputController.Reload_Button)
        {
            Debug.Log("Reloading");
            Atrributes.Animation.Play("HandGunReload");
            Atrributes.ReloadSound.Play();
            yield return new WaitForSeconds(4.5f);
        
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

            Debug.Log("Done");
        }
    }

    public void Shoot()
    {
        if(InputController.Left_Mouse && Time.time >= timeToFireAllowed 
            && Atrributes.CurrentAmmoInCip >= 1)
        {  
            Atrributes.CurrentAmmoInCip -= 1;

            timeToFireAllowed = Time.time + 1 / Atrributes.RateOfFire;
            Atrributes.ShotSound.Play();
            Atrributes.Animation.Play();
            SpawnProjectile();

            if(hit.distance <= Atrributes.Range)
            {
                if(tag == "Enemy")
                {
                    GiveDamage(ref hit, Atrributes.Damage);                
                }
            }
        }
    }

    public void SpawnProjectile()
    {
        GameObject vfx;

        if(Atrributes.ShotPoint != null)
        {
            vfx = Instantiate(effectToSpawn, Atrributes.ShotPoint.position, Atrributes.ShotPoint.rotation);
        }
        else
        {
            Debug.Log("No Fire Point!");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Atrributes.Animation = GetComponentInParent<Animation>();
    }

    void Awake()
    {
        effectToSpawn = Atrributes.VFX[0];
    }

    // Update is called once per frame
    void Update()
    {
        SetInput();
        Shoot();
        StartCoroutine(Reload()); 
    }

    void FixedUpdate()
    {
        if(Physics.Raycast(Atrributes.ShotPoint.position, transform.TransformDirection(Vector3.forward), out hit, Atrributes.Range))
        { 
            Debug.Log(tag);      
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
        InputController.Reload_Button = Input.GetButtonDown("Reload");
    }
}
