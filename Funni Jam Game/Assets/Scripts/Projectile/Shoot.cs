using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject projectile;
    public LayerMask shootLayerMask;

    public GameObject projectileRoute;
    public ProjectileRoute projectileRouteScript;
    Vector3 aimPoint;


    void Start()
    {
        projectileRoute = GameObject.FindGameObjectWithTag("ProjectileRoute");
        projectileRouteScript = projectileRoute.GetComponent<ProjectileRoute>();
    }

    private void Update()
    {
        aimPoint = transform.position + transform.forward * 10.5f;
        Debug.Log(aimPoint);
    }

    protected void ShootProjectile()
    {
        //Sets up the route for the bullet
        projectileRouteScript.Setup(transform.position, WhereToShoot());

        //Instanitates the bullet and sets variables
        GameObject new_projectile = Instantiate(projectile);
        new_projectile.transform.position = transform.position;
        new_projectile.GetComponent<Projectile>().routes[0] = projectileRoute.transform;

        //Sets bullet speed based off of how far away the bullet target is
        new_projectile.GetComponent<Projectile>().speedModifier = BulletSpeed();
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
            return aimPoint;
        }
    }

    RaycastHit2D ShootRay()
    {
        //Shoots a ray in the direction of the mouse
        Vector3 raycastDir = aimPoint - transform.position;
        float distance = Vector2.Distance(transform.position, aimPoint);

        return Physics2D.Raycast(transform.position, raycastDir, distance, shootLayerMask);
    }

    float BulletSpeed()
    {
        //Calculates how fast the bullet should go based off of distance from the player
        float bulletSpeed = 2f;
        float x = 10.5f - (Vector2.Distance(transform.position, WhereToShoot()));
        x /= 10.5f;
        bulletSpeed += x;
        return bulletSpeed;
    }
}
