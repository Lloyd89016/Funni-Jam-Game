using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : Shoot
{
    public float coolDownSpeed;
    float cooldown = .08f;
    public Transform aimPoint;

    void Update()
    {
        //Cooldown
        cooldown -= coolDownSpeed * Time.deltaTime;

        if (cooldown <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                cooldown = 1;
                ShootProjectile(aimPoint.position);
            }
        }
    }
}
