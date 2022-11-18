using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject projectile;
    public LayerMask shootLayerMask;

    public GameObject projectileRoute;
    public ProjectileRoute projectileRouteScript;

    void Start()
    {
        projectileRouteScript = projectileRoute.GetComponent<ProjectileRoute>();
    }

    protected void ShootProjectile(Vector3 aimPoint)
    {
        //Sets up the route for the bullet
        projectileRouteScript.Setup(transform.position, WhereToShoot(aimPoint));

        //Instanitates the bullet and sets variables
        GameObject new_projectile = Instantiate(projectile);
        new_projectile.transform.position = transform.position;
        new_projectile.GetComponent<Projectile>().routes[0] = projectileRoute.transform;

        //Sets bullet speed based off of how far away the bullet target is
        new_projectile.GetComponent<Projectile>().speedModifier = BulletSpeed(aimPoint);
    }

    Vector2 WhereToShoot(Vector3 aimPoint)
    {
        //Figures out the end point of the bullet
        RaycastHit2D hit = ShootRay(aimPoint);
        if(hit == true)
        {
            return hit.point;
        }
        else
        {
            return aimPoint;
        }
    }

    RaycastHit2D ShootRay(Vector3 aimPoint)
    {
        //Shoots a ray in the direction of the mouse
        Vector3 raycastDir = aimPoint - transform.position;
        float distance = Vector2.Distance(transform.position, aimPoint);

        return Physics2D.Raycast(transform.position, raycastDir, distance, shootLayerMask);
    }

    float BulletSpeed(Vector3 aimPoint)
    {
        //Calculates how fast the bullet should go based off of distance from the player
        float bulletSpeed = 2f;
        float x = Vector2.Distance(aimPoint, transform.position) - (Vector2.Distance(transform.position, WhereToShoot(aimPoint)));
        x /= Vector2.Distance(aimPoint, transform.position);
        bulletSpeed += x;
        return bulletSpeed;
    }
}
