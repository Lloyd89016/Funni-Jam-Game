using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    void Start()
    {
        Explode();
    }

    void Explode()
    {
        //Gets all of the objects in the explosion radius, then applys explosion fource.
        Vector3 explosionPos = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, 1);
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
