using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public GameObject route;
    Route routeScript;
    public Transform gunAimPoint;
    public float cooldown = .08f;

    void Start()
    {
        routeScript = route.GetComponent<Route>();
    }

    void Update()
    {
        cooldown -= 1f * Time.deltaTime;

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
        GameObject Bullet = Instantiate(bullet);
        Bullet.transform.position = transform.position;
        Bullet.GetComponent<Bullet>().routes[0] = route.transform;

        //Sets bullet speed based off of how far away the bullet target is
        //Bullet.GetComponent<Bullet>().speedModifier = 3 + (4.3f - (Vector2.Distance(transform.position, WhereToShoot()) / 2.5f));
        Bullet.GetComponent<Bullet>().speedModifier = BulletSpeed();
    }

    Vector2 WhereToShoot()
    {
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
        Vector3 raycastDir = gunAimPoint.position - transform.position;
        float distance = Vector2.Distance(transform.position, gunAimPoint.position);

        return Physics2D.Raycast(transform.position, raycastDir, distance);
    }

    float BulletSpeed()
    {
        float bulletSpeed = 3;
        float x = 13f - (Vector2.Distance(transform.position, WhereToShoot()));
        x /= 13;
        bulletSpeed += x;
        return bulletSpeed;
    }
}
