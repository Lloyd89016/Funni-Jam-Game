using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    //Explosion settings
    public float radius = 5.0F;
    public float power = 10.0F;

    private ParticleSystem particleEffect;
    public MonoBehaviour a;

    void Start()
    {
        particleEffect = GetComponent<ParticleSystem>();

        Explode();
    }

    void Update()
    {
        if (particleEffect.isStopped)
        {
            Destroy(gameObject);
        }
    }

    void Explode()
    {
        //Gets all of the objects in the explosion radius, then applys explosion fource.
        Vector3 explosionPos = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, radius);
        foreach (Collider2D hit in colliders)
        {
            DealDamage(hit);
            AddExplosionForce(hit, explosionPos);
            DisableScripts(hit);
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
            new_healthScript.health -= 1;
        }
    }

    void DisableScripts(Collider2D hit)
    {
        TopDownMovement001 playerMovement = hit.gameObject.GetComponent<TopDownMovement001>();
        if (playerMovement != null)
        {

        }
        //MonoBehaviour enemyMovement = hit.gameObject.GetComponent<>();
    }
}
