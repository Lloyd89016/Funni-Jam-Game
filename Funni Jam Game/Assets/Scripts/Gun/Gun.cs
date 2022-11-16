using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public GameObject route;
    Route routeScript;
    public Transform gunAimPoint;
    public LayerMask shootLayerMask;

    public float coolDownSpeed;
    float cooldown = .08f;

    void Start()
    {
        routeScript = route.GetComponent<Route>();
    }

    void Update()
    {
        //Cooldown
        cooldown -= coolDownSpeed * Time.deltaTime;

        if (cooldown <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                cooldown = 1;
                SpawnBullet();
            }
        }
  
    }

    void SpawnBullet()
    {
        //Sets up the route for the bullet
        routeScript.Setup(WhereToShoot());

        //Instanitates the bullet and sets variables
        GameObject new_bullet = Instantiate(bullet);
        new_bullet.transform.position = transform.position;
        new_bullet.GetComponent<Bullet>().routes[0] = route.transform;

        //Sets bullet speed based off of how far away the bullet target is
        new_bullet.GetComponent<Bullet>().speedModifier = BulletSpeed();
    }

    Vector2 WhereToShoot()
    {
        //Figures out the end point of the bullet
        RaycastHit2D hit = ShootRay();
        if(hit == true)
        {
            return hit.point;
        }
        else
        {
            return gunAimPoint.position;
        }
    }

    RaycastHit2D ShootRay()
    {
        //Shoots a ray in the direction of the mouse
        Vector3 raycastDir = gunAimPoint.position - transform.position;
        float distance = Vector2.Distance(transform.position, gunAimPoint.position);

        return Physics2D.Raycast(transform.position, raycastDir, distance, shootLayerMask);
    }

    float BulletSpeed()
    {
        //Calculates how fast the bullet should go based off of distance from the player
        float bulletSpeed = 3;
        float x = 13f - (Vector2.Distance(transform.position, WhereToShoot()));
        x /= 13;
        bulletSpeed += x;
        return bulletSpeed;
    }
}
