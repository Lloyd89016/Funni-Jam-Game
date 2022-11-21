using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    //Spawned in when none exploding bullets hit somthing
    //Does damage to a small area

    private ParticleSystem particleEffect;
    public LayerMask enemyLayerMask;

    void Start()
    {
        particleEffect = GetComponent<ParticleSystem>();
        Explode();
    }

    private void Update()
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
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, transform.localScale.x, enemyLayerMask);
        foreach (Collider2D hit in colliders)
        {
            DealDamage(hit);
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
}
