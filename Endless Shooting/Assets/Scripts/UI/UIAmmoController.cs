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
        }
        
        Debug.Log(Type);
    }
}
