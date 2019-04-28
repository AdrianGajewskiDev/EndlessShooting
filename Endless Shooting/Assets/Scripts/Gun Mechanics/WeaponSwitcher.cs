using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public List<GameObject> weapons;

    void Update()
    {
        InputController.HandGun = Input.GetKeyDown(KeyCode.Alpha1);
        InputController.Rifle = Input.GetKeyDown(KeyCode.Alpha2);
        InputController.LaserGun = Input.GetKeyDown(KeyCode.Alpha3);
        InputController.SniperGun = Input.GetKeyDown(KeyCode.Alpha4);
        SwitchWeapon();
    }

    void SwitchWeapon()
    {
         if(InputController.HandGun)
        {
            weapons[0].SetActive(true);
            weapons[1].SetActive(false);
            weapons[2].SetActive(false);
            weapons[3].SetActive(false);
        }
        
        if(InputController.Rifle)
        {
            weapons[0].SetActive(false);
            weapons[1].SetActive(true);
            weapons[2].SetActive(false);
            weapons[3].SetActive(false);
        }

        if(InputController.LaserGun)
        {
            weapons[0].SetActive(false);
            weapons[1].SetActive(false);
            weapons[2].SetActive(true);
            weapons[3].SetActive(false);
        }

        if(InputController.SniperGun)
        {
            weapons[0].SetActive(false);
            weapons[1].SetActive(false);
            weapons[2].SetActive(false);
            weapons[3].SetActive(true);
        }
    }
}
