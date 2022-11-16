using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : Health
{
    public GameObject destroyedObject;

    void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameObject new_explosion = Instantiate(destroyedObject);
        new_explosion.transform.position = transform.position;

        Destroy(gameObject);
    }

}