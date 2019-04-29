using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAmmoController : MonoBehaviour
{
    public static GunType Type;
    public Text AmmoCounter;
    GameObject CurrentWeaponScriptHolder;
    public GameObject WeaponsScriptHolder;
    public GameObject SMGWeaponScriptHolder;
    public GameObject LaserWeaponScriptHolder;
    public GameObject SniperWeaponScriptHolder;

    // Update is called once per frame
    void Update()
    {

        if(Type == GunType.HandGun)
        {
            CurrentWeaponScriptHolder = WeaponsScriptHolder;
        }
        else if(Type == GunType.SMGGun)
        {
            CurrentWeaponScriptHolder = SMGWeaponScriptHolder;
        }
        else if(Type == GunType.LaserGun)
        {
            CurrentWeaponScriptHolder = LaserWeaponScriptHolder;
        }
        else if(Type == GunType.Sniper)
        {
            CurrentWeaponScriptHolder = SniperWeaponScriptHolder;
        }
        else if(Type == GunType.Granade)
        {
            CurrentWeaponScriptHolder = null;
        }

        switch(Type)
        {
            case GunType.HandGun:
            {
                var script = CurrentWeaponScriptHolder.GetComponent<HandGun>();
                AmmoCounter.text = script.Atrributes.CurrentAmmoInCip + "/" + script.Atrributes.MaxAmmo;
            }break;

            
            case GunType.SMGGun:
            {
                var script = CurrentWeaponScriptHolder.GetComponent<SMGGun>();
                AmmoCounter.text = script.Atrributes.CurrentAmmoInCip + "/" + script.Atrributes.MaxAmmo;
            }break;

            case GunType.Arms:
            {
                AmmoCounter.text = string.Empty;
            }
            break;

            case GunType.LaserGun:
            {
                var script = CurrentWeaponScriptHolder.GetComponent<LaserGun>();
                AmmoCounter.text = script.Atrributes.CurrentAmmoInCip + "/" + script.Atrributes.MaxAmmo;
            }break;

            case GunType.Sniper:
            {
                var script = CurrentWeaponScriptHolder.GetComponent<SniperGun>();
                AmmoCounter.text = script.Atributes.CurrentAmmoInCip + "/" + script.Atributes.MaxAmmo;
            }break;

            case GunType.Granade:
            {
                AmmoCounter.text = "-/-";
            }break;
        }
        
    }
}
