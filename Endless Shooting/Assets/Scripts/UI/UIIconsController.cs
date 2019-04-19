using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIIconsController : MonoBehaviour
{
    public Image HandGunIcon;
    public Image SMGGunIcon;

    //  Update is called once per frame
    void Update()
    {
        switch(UIAmmoController.Type)
        {
            case GunType.HandGun:
            {
                SMGGunIcon.gameObject.SetActive(false);
                HandGunIcon.gameObject.SetActive(true);
            }break;

            case GunType.SMGGun:
            {
                HandGunIcon.gameObject.SetActive(false);
                SMGGunIcon.gameObject.SetActive(true);
            }break;
        }
    }
}
