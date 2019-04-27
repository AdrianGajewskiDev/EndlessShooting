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

    //  Update is called once per frame
    void Update()
    {
        switch(UIAmmoController.Type)
        {
            case GunType.HandGun:
            {
                LaserGunIcon.gameObject.SetActive(false);
                SMGGunIcon.gameObject.SetActive(false);
                SniperGunIcon.gameObject.SetActive(false);
                HandGunIcon.gameObject.SetActive(true);
            }break;

            case GunType.SMGGun:
            {
                LaserGunIcon.gameObject.SetActive(false);
                HandGunIcon.gameObject.SetActive(false);
                SniperGunIcon.gameObject.SetActive(false);
                SMGGunIcon.gameObject.SetActive(true);
            }break;

            case GunType.LaserGun:
            {
                LaserGunIcon.gameObject.SetActive(true);
                HandGunIcon.gameObject.SetActive(false);
                SniperGunIcon.gameObject.SetActive(false);
                SMGGunIcon.gameObject.SetActive(false);
            }break;

            case GunType.Sniper:
            {
                LaserGunIcon.gameObject.SetActive(false);
                HandGunIcon.gameObject.SetActive(false);
                SniperGunIcon.gameObject.SetActive(true);
                SMGGunIcon.gameObject.SetActive(false);
            }break;
        }
    }
}
