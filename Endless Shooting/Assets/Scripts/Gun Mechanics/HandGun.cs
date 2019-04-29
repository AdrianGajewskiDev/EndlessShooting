using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGun : MonoBehaviour, IGun
{
    public GunAtrributes Atrributes;
    float timeToFireAllowed;
    GameObject effectToSpawn;
    RaycastHit hit;
    Ray ray;
    string tag = string.Empty;

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
    
        if(InputController.Reload_Button)
        {
            if(Atrributes.MaxAmmo >= 1)
            {

            
                Debug.Log("Reloading");
                Atrributes.Animation.Play("HandGunReload");
                Atrributes.ReloadSound.Play();
                GetComponentInParent<HandGun>().enabled = false;
                yield return new WaitForSeconds(4f);
                GetComponentInParent<HandGun>().enabled = true;
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
        if(InputController.Left_Mouse && Time.time >= timeToFireAllowed 
            && Atrributes.CurrentAmmoInCip >= 1)
        {  
            AnimateCrosshairs();
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

    void AnimateCrosshairs()
    {
        Atrributes.Crosshair.GetComponent<Animation>().Play("CrosshairShotAnim");
    }


    // Start is called before the first frame update
    void Start()
    {
        Atrributes.Animation = GetComponentInParent<Animation>();
        Atrributes.Crosshair.SetActive(true);
    }

    void Awake()
    {
        effectToSpawn = Atrributes.VFX[0];
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(GameObject.Find("SmallExplosionEffect(Clone)"),0.02f);
        
        SetInput();
        Shoot();
        StartCoroutine(Reload()); 
        UIAmmoController.Type = Atrributes.Type;
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
    void SetInput()
    {
        InputController.Left_Mouse = Input.GetButtonDown("Fire1");
        InputController.Reload_Button = Input.GetButtonDown("Reload");
    }

    // void OnDisabled()
    // {
    //     UIAmmoController.Type = GunType.Arms
    // }
}
