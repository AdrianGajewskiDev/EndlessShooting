using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Granade : MonoBehaviour
{
    public int damage;
    public float timeToExplosion = 5f;
    public float force = 5f;
    public float range = .1f;
    Transform explodedPosition;
    bool throwed = false;
    public AudioSource explodeSound;
    public ParticleSystem explodeEffect;
    private Rigidbody rbody;

    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        UIAmmoController.Type = GunType.Granade;
        throwed = false;
    }

    void Update()
    {
        UIAmmoController.Type = GunType.Granade;
        InputController.Granade = Input.GetKeyDown(KeyCode.G);

        if(throwed == true)
            timeToExplosion -= Time.deltaTime;

        Throw();
        if(timeToExplosion <= 0 && throwed == true)
        {
            StartCoroutine(Explode());
            throwed = false;
        }
    }

    void Throw()
    {
        if(InputController.Granade && throwed == false)
        {
            transform.parent = null;
            rbody.isKinematic = false;
            rbody.AddForce(transform.forward * force, ForceMode.Impulse); 
            throwed = true;
        }
    }
    IEnumerator Explode()
    {
        if(!explodeEffect.isPlaying)
        {
            explodeEffect.Play();            
        }

        GiveDamage();
        explodeSound.Play();

        yield return new WaitForSeconds(1.8f);
        Destroy(gameObject);

        
    }

    void GiveDamage()
    {
        explodedPosition = transform;

        Collider[] coll = Physics.OverlapSphere(transform.position, range);

        foreach(Collider col in coll )
        {
            Debug.Log("Collieded with: " + col.gameObject.name);
            Rigidbody rb = col.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(damage, transform.position, range);
            }

            IHealth eh = col.GetComponent<EnemyHealth>();
            if(eh != null)
            {
                eh.GetDamage(damage);
            }
        }
    }

}
