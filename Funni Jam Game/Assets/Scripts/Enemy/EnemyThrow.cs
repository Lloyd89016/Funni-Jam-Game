using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThrow : Shoot
{
    [SerializeField] Transform player;
    [SerializeField] LayerMask wallLayerMask;
    Vector3 offset;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("CanShoot", Random.Range(.005f, 1.005f), 1);

        //Sets the offset that will be added to the player pos to get where the ai shoots
        offset = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
    }

    void CanShoot()
    {
        float dist = Vector2.Distance(player.position + offset, transform.position);
        if(dist <= 15 && dist >= 4 && DetectWall() == false)
        {
            ShootProjectile(player.position + offset);
        }
    }

    RaycastHit2D DetectWall()
    {
        //Shoots a ray in the direction of the player to make sure it is not shooting at a wall
        Vector3 raycastDir = player.position + offset - transform.position;
        float distance = Vector2.Distance(player.position + offset, transform.position);

        return Physics2D.Raycast(transform.position, raycastDir, distance, wallLayerMask);
    }
}
