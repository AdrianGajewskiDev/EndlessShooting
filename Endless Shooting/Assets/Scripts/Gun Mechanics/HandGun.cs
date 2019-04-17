using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGun : MonoBehaviour, IGun
{
    public GunAtrributes Atrributes;
    float timeToFireAllowed;
    GameObject effectToSpawn;

    public void GiveDamage(ref RaycastHit hit)
    {
        
    }

    public IEnumerator Reload()
    {
        yield return null;
    }

    public void Shoot()
    {
        if(InputController.Left_Mouse && Time.time >= timeToFireAllowed)
        {
            timeToFireAllowed = Time.time + 1 / Atrributes.RateOfFire;
            Atrributes.ShotSound.Play();
            Atrributes.Animation.Play();
            SpawnProjectile();
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
        effectToSpawn = Atrributes.VFX[0];
    }

    // Update is called once per frame
    void Update()
    {
        InputController.Left_Mouse = Input.GetButtonDown("Fire1");
        Shoot();
    }
}
