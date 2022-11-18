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
        if(dist <= 15 && DetectWall() == false)
        {
            ShootProjectile(player.position);
        }
    }

    RaycastHit2D DetectWall()
    {
        //Shoots a ray in the direction of the player to make sure it is not shooting at a wall
        Vector3 raycastDir = player.position - transform.position;
        float distance = Vector2.Distance(player.position, transform.position);

        return Physics2D.Raycast(transform.position, raycastDir, distance, wallLayerMask);
    }
}
