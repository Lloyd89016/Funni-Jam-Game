using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThrow : Shoot
{
    [SerializeField] Transform player;
    [SerializeField] LayerMask wallLayerMask;
    Vector3 offset;
    [SerializeField] PlayerHealth playerHealth;
    void Start()
    {
        maxCooldown = cooldown;
        cooldown = 0;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("CanShoot", Random.Range(.005f, 1.005f), 1);

        //Sets the offset that will be added to the player pos to get where the ai shoots
        offset = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
    }

    public int damage = 1;
    [SerializeField] float cooldown = 10f;
    [SerializeField] float maxCooldown = 10f;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (cooldown <= 0)
        {
            if (collision.gameObject.tag == ("Player"))
            {
                playerHealth.TakeDamage(damage);
                cooldown = maxCooldown;
            }
        }

    }
    private void Update()
    {
        cooldown -= .1f * Time.deltaTime;
    }
    void CanShoot()
    {
        float dist = Vector2.Distance(player.position + offset, transform.position);
        if(dist <= 15 && dist >= .5 && DetectWall() == false)
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
