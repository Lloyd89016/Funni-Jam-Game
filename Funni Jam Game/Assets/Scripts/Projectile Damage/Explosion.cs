using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    //Explosion settings
    [SerializeField] float radius = 5.0F;
    [SerializeField] float power = 10.0F;

    [SerializeField] LayerMask targetLayermask;

    private ParticleSystem particleEffect;

    void Start()
    {
        particleEffect = GetComponent<ParticleSystem>();

        Explode();
    }

    void Update()
    {
        //When the particle effect is done playing the object gets destoryed
        if (particleEffect.isStopped)
        {
            Destroy(gameObject);
        }
    }

    void Explode()
    {
        //Gets all of the objects in the explosion radius, then applys explosion fource.
        Vector3 explosionPos = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, radius, targetLayermask);
        foreach (Collider2D hit in colliders)
        {
            DealDamage(hit);
            AddExplosionForce(hit, explosionPos);
            DisableMovement(hit);
        }
    }

    void AddExplosionForce(Collider2D hit, Vector3 explosionPos)
    {
        Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            //There is no build "AddExplosionForce2D", so this uses a custom extention
            rb.AddExplosionForce2D(power, explosionPos, radius);
        }
    }

    void DealDamage(Collider2D hit)
    {
        Health new_healthScript = hit.gameObject.GetComponent<Health>();
        if (new_healthScript != null)
        {
            if(Vector2.Distance(transform.position, hit.gameObject.transform.position) <= 2)
            {
                new_healthScript.health -= 1;
            }
        }
    }

    void DisableMovement(Collider2D hit)
    {
        DisableMovement disableMovement = hit.gameObject.GetComponent<DisableMovement>();
        if (disableMovement != null)
        {
            disableMovement.DisableScript();
        }
    }
}
