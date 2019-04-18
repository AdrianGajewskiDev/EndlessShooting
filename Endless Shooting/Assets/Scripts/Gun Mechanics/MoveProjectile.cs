using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveProjectile : MonoBehaviour
{
    public float speed;
    public GameObject muzzle;
    public GameObject hit;
    // Update is called once per frame
    void Update()
    {
        if(speed != 0)
        {
            transform.position += transform.forward * (speed * Time.deltaTime);
        }
        else
        {
            Debug.Log("No Speed");
        }

        Destroy(GameObject.Find("Projectile(Clone)"), 10);
    }

    void OnCollisionEnter(Collision col)
    {
        speed = 0;
        ContactPoint contact = col.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;

        if(hit != null)
        {
            var hitVFX = Instantiate(hit, pos, rot);
        }
        Destroy(gameObject);
    }
}
