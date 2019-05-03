using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIIconsController : MonoBehaviour
{
    public Image HandGunIcon;
    public Image SMGGunIcon;
    public Image LaserGunIcon;
    public Image SniperGunIcon;
    public Image GrenadeIcon;

    public Image[] bloodEffect;

    private DamageAmount amount;
    enum DamageAmount
    {
        none,
        small,
        medium,
        large
    }

    //  Update is called once per frame
    void Update()
    {

        // if(PlayerHealth.Health >= 25)
        //     amount = DamageAmount.none;
        // else if(PlayerHealth.Health <= 20 && PlayerHealth.Health >= 15)
        //     amount = DamageAmount.small;
        // else if(PlayerHealth.Health < 15 && PlayerHealth.Health >= 10)
        //     amount = DamageAmount.medium;
        // else if(PlayerHealth.Health < 10 && PlayerHealth.Health >= 5)
        //     amount = DamageAmount.large;
        

        switch(UIAmmoController.Type)
        {
            case GunType.HandGun:
            {
                LaserGunIcon.gameObject.SetActive(false);
                SMGGunIcon.gameObject.SetActive(false);
                SniperGunIcon.gameObject.SetActive(false);
                HandGunIcon.gameObject.SetActive(true);
                GrenadeIcon.gameObject.SetActive(false);

            }break;

            case GunType.SMGGun:
            {
                LaserGunIcon.gameObject.SetActive(false);
                HandGunIcon.gameObject.SetActive(false);
                SniperGunIcon.gameObject.SetActive(false);
                SMGGunIcon.gameObject.SetActive(true);
                GrenadeIcon.gameObject.SetActive(false);
            }break;

            case GunType.LaserGun:
            {
                LaserGunIcon.gameObject.SetActive(true);
                HandGunIcon.gameObject.SetActive(false);
                SniperGunIcon.gameObject.SetActive(false);
                SMGGunIcon.gameObject.SetActive(false);
                GrenadeIcon.gameObject.SetActive(false);
            }break;

            case GunType.Sniper:
            {
                LaserGunIcon.gameObject.SetActive(false);
                HandGunIcon.gameObject.SetActive(false);
                SniperGunIcon.gameObject.SetActive(true);
                SMGGunIcon.gameObject.SetActive(false);
                GrenadeIcon.gameObject.SetActive(false);
            }break;

            case GunType.Granade:
            {
                LaserGunIcon.gameObject.SetActive(false);
                HandGunIcon.gameObject.SetActive(false);
                SniperGunIcon.gameObject.SetActive(false);
                SMGGunIcon.gameObject.SetActive(false);
                GrenadeIcon.gameObject.SetActive(true);
            }break;
        }

        switch(amount)
        {
            case DamageAmount.none:
            {
                foreach(var img in bloodEffect)
                {
                    img.gameObject.SetActive(false);
                }
            }break;

            case DamageAmount.small:
            {
                bloodEffect[0].gameObject.SetActive(true);
                bloodEffect[1].gameObject.SetActive(false);
                bloodEffect[2].gameObject.SetActive(false);
            }break;

            case DamageAmount.medium:
            {
                bloodEffect[0].gameObject.SetActive(false);
                bloodEffect[1].gameObject.SetActive(true);
                bloodEffect[2].gameObject.SetActive(false);
            }break;

            case DamageAmount.large:
            {
                bloodEffect[0].gameObject.SetActive(false);
                bloodEffect[1].gameObject.SetActive(false);
                bloodEffect[2].gameObject.SetActive(true);
            }break;
        }
    }

   
}
