using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public LayerMask enemyLayerMask;

    private void Update()
    {
        CheckIfTouching();
    }

    void CheckIfTouching()
    {
        //Gets all of the objects in the explosion radius, then applys explosion fource.
        Vector3 explosionPos = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, transform.localScale.x, enemyLayerMask);
        foreach (Collider2D hit in colliders)
        {
            DealDamage();
        }
    }

    void DealDamage()
    {
        Health new_healthScript = gameObject.GetComponent<Health>();
        if (new_healthScript != null)
        {
            new_healthScript.health -= 1;
        }
    }
}
