using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public GameObject route;
    Route routeScript;
    public Transform gunAimPoint;

    void Start()
    {
        routeScript = route.GetComponent<Route>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            SpawnBullet();
        }
    }

    void SpawnBullet()
    {
        routeScript.Setup(WhereToShoot());

        GameObject Bullet = Instantiate(bullet);
        Bullet.transform.position = transform.position;
        Bullet.GetComponent<Bullet>().routes[0] = route.transform;

        Bullet.GetComponent<Bullet>().speedModifier = 2 + (4.3f - (Vector2.Distance(transform.position, WhereToShoot()) / 2.5f));
    }

    Vector2 WhereToShoot()
    {
        RaycastHit2D hit = shootRay();
        if(hit == true)
        {
            return hit.point;
        }
        else
        {
            return gunAimPoint.position;
        }
    }

    RaycastHit2D shootRay()
    {
        Vector3 raycastDir = gunAimPoint.position - transform.position;
        float distance = Vector2.Distance(transform.position, gunAimPoint.position);

        return Physics2D.Raycast(transform.position, raycastDir, distance);
    }
}