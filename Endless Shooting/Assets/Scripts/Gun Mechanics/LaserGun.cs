using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour, IGun
{

    public GunAtrributes Atrributes;
    GameObject lazer;
    RaycastHit hit;
    string tag = string.Empty;

    [SerializeField] ParticleSystem hitEffect;

    public void GiveDamage(ref RaycastHit ray, int damageAmount)
    {
        if(ray.transform != null)
        {
            if(ray.transform.GetComponent<EnemyHealth>() != null)
            {
                StartCoroutine(GiveDamage_Laser(ray, damageAmount));
            }
        }
    }

    IEnumerator GiveDamage_Laser(RaycastHit hit, int amount)
    {
        hit.transform.GetComponent<EnemyHealth>().GetDamage(amount);
        yield return new WaitForSeconds(2);
    }
    public IEnumerator Reload()
    {
        if(InputController.Reload_Button && Atrributes.CurrentAmmoInCip < Atrributes.ClipSize)
        {
            if(Atrributes.MaxAmmo >= 1)
            {

            
                Debug.Log("Reloading");
                Atrributes.Animation.Play("LazerGunReload");
                Atrributes.ReloadSound.Play();
                GetComponentInParent<LaserGun>().enabled = false;
                yield return new WaitForSeconds(4f);
                GetComponentInParent<LaserGun>().enabled = true;
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
            else
            {
                Debug.Log("No Ammo");
            }

            Debug.Log("Done");
        }
    }

    void SpawnHitEffect()
    {
        if(hit.transform != null)
        {
            var effectToSpawn = hitEffect;
            Instantiate(effectToSpawn, hit.point, hit.transform.rotation);
        }
        
    }

    public void Shoot()
    {
        if(InputController.Left_Mouse && Atrributes.CurrentAmmoInCip >= 1)
        {
            SpawnProjectile();
            UpdateLazer();
            SpawnHitEffect();
            StartCoroutine(TakeOneAmmoPoint());

            if(!Atrributes.ShotSound.isPlaying)
                Atrributes.ShotSound.Play();

            if(tag == "Enemy")
            {
                GiveDamage(ref hit, Atrributes.Damage);
            }
        }

        if((Input.GetMouseButtonUp(0)))
        {
            DisableLazer();
            Atrributes.ShotSound.Stop();
        }
    }

    IEnumerator TakeOneAmmoPoint()
    {
        Atrributes.CurrentAmmoInCip -= 1;
        yield return new WaitForSecondsRealtime(2);
    }
    public void SpawnProjectile()
    {
        lazer.SetActive(true);
    }

    void Start()
    {
        UIAmmoController.Type = Atrributes.Type;
        Atrributes.Crosshair.SetActive(false);
        Atrributes.Animation = GetComponentInParent<Animation>();
        lazer = Instantiate(Atrributes.VFX[0], Atrributes.ShotPoint);
        DisableLazer();
    }

    void FixedUpdate()
    {
        if(Physics.Raycast(Atrributes.ShotPoint.transform.position, Atrributes.ShotPoint.TransformDirection(Vector3.forward), out hit, Atrributes.Range))
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
    void DisableLazer()
    {
        lazer.SetActive(false);
    }

    void UpdateLazer()
    {
        lazer.transform.position = Atrributes.ShotPoint.position;
    }
    void Update()
    {
        UIAmmoController.Type = Atrributes.Type;

        Shoot();
        SetInput();
        StartCoroutine(Reload());

        if(Atrributes.CurrentAmmoInCip < 1)
        {
            Atrributes.ShotSound.Stop();
            DisableLazer();
        }

        Destroy(GameObject.Find("SmallExplosionEffect(Clone)"),0.02f);
    }

    void SetInput()
    {
        InputController.Left_Mouse = Input.GetMouseButton(0);
        InputController.Reload_Button = Input.GetButtonDown("Reload");
    }
}
