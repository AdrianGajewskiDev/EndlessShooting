using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGun : MonoBehaviour, IGun
{
    public GunAtrributes Atrributes;
    float timeToFireAllowed;
    GameObject effectToSpawn;
    RaycastHit hit;

    public void GiveDamage(ref RaycastHit ray, int damageAmount)
    {
        ray.transform.gameObject.SendMessage("GetDamage", damageAmount, SendMessageOptions.DontRequireReceiver);
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

            if(hit.distance <= Atrributes.Range)
            {
                if(hit.transform.tag == "Enemy")
                    GiveDamage(ref hit, Atrributes.Damage);
                else if(hit.transform.tag == null)
                    return;      
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
        InputController.Left_Mouse = Input.GetButtonDown("Fire1");
        Shoot();

        if(Physics.Raycast(Atrributes.ShotPoint.position, transform.TransformDirection(Vector3.forward), out hit, Atrributes.Range))
        { 
            Debug.Log(hit.transform.tag);      
        }
        effectToSpawn = Atrributes.VFX[0];
    }
}
