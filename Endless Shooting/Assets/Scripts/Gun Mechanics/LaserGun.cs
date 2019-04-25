using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour, IGun
{

    public GunAtrributes Atrributes;
    GameObject lazer;

    public void GiveDamage(ref RaycastHit ray, int damageAmount)
    {
        throw new System.NotImplementedException();
    }

    public IEnumerator Reload()
    {
        throw new System.NotImplementedException();
    }

    public void Shoot()
    {
        if(InputController.Left_Mouse)
        {
            SpawnProjectile();
            UpdateLazer();
            if(!Atrributes.ShotSound.isPlaying)
                Atrributes.ShotSound.Play();
        }

        if((Input.GetMouseButtonUp(0)))
        {
            DisableLazer();
            Atrributes.ShotSound.Stop();
        }
    }

    public void SpawnProjectile()
    {
        lazer.SetActive(true);
    }

    void Start()
    {
        UIAmmoController.Type = Atrributes.Type;
        Atrributes.Crosshair.SetActive(false);
        lazer = Instantiate(Atrributes.VFX[0], Atrributes.ShotPoint);
        DisableLazer();
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
        Shoot();
        SetInput();
    }

    void SetInput()
    {
        InputController.Left_Mouse = Input.GetMouseButton(0);
    }
}
