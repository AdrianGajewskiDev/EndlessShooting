using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAmmoController : MonoBehaviour
{
    public static GunType Type;
    public Text AmmoCounter;
    public GameObject WeaponsScriptHolder;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(Type)
        {
            case GunType.HandGun:
            {
                var script = WeaponsScriptHolder.GetComponent<HandGun>();
                AmmoCounter.text = script.Atrributes.CurrentAmmoInCip + "/" + script.Atrributes.MaxAmmo;
            }break;
        }
        
        Debug.Log(Type);
    }
}
