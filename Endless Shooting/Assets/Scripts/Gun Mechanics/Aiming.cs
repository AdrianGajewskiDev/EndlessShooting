using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    bool IsAiming = false;
    public float aimSpeed;
    public Vector3 NormalPosition;
    public Vector3 AimPosition;
    

    void Update()
    {
        InputController.Right_Mouse = Input.GetMouseButton(1);
        Aim();
    }

    void Aim()
    {
        if( InputController.Right_Mouse && IsAiming == false)
        {
            transform.localPosition = AimPosition;
            IsAiming = true;
        }

        if(!InputController.Right_Mouse && IsAiming == true)
        {
            transform.localPosition = NormalPosition;
            IsAiming = false;
        }
    }
}
