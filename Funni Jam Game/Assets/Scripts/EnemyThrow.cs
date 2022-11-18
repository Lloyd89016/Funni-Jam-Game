using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThrow : Shoot
{
    public Transform player;
    public LayerMask wallLayerMask;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("CanShoot", Random.Range(.005f, 1.005f), 1);
    }
    
    void CanShoot()
    {
        float dist = Vector2.Distance(player.position, transform.position);
        if(dist <= 7 && DetectWall() == false)
        {
            ShootProjectile();
        }
    }

    RaycastHit2D DetectWall()
    {
        //Shoots a ray in the direction of the player
        Vector3 raycastDir = player.position - transform.position;
        float distance = Vector2.Distance(transform.position, transform.position + transform.forward * 10.5f);

        return Physics2D.Raycast(transform.position, raycastDir, distance, shootLayerMask);
    }
}
