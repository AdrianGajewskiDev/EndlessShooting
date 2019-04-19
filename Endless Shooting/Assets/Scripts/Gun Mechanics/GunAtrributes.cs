using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct GunAtrributes
{
    public string Name;
    public int ClipSize;
    public int MaxAmmo;
    public int CurrentAmmoInCip;
    public int Damage;
    public float RateOfFire;
    public float Range;
    [HideInInspector] public Animation Animation;
    public AudioSource ShotSound;
    public AudioSource ReloadSound;
    public Transform ShotPoint;
    public GameObject Crosshair;
    public GameObject[] Crosshairs;
    public GameObject[] VFX;

}
