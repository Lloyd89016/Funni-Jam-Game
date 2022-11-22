using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMelee : Shoot
{
    [SerializeField] float Cooldown = .1f;
    public int Damage = 1;
    PlayerHealth player;
    private float maxCooldown;

    private void Start()
    {
        maxCooldown = Cooldown;
        Cooldown = 0;

    }

    private void Update()
    {
        Cooldown -= .1f * Time.deltaTime;

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Cooldown <= 0)
        {
            if (collision.gameObject.tag == "Player")
            {
                Health new_healthScript = collision.gameObject.GetComponent<Health>();
                if (new_healthScript != null)
                {
                    if (Vector2.Distance(transform.position, collision.gameObject.transform.position) <= 2)
                    {
                        new_healthScript.health -= Damage;
                    }
                }
            }

        }
    }
}
  
