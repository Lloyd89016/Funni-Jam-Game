using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    public Transform[] routes;

    private int routeToGo;

    private float tParam;

    private Vector2 objectPosition;

    public float speedModifier;
    //Rotate Stuff
    private Vector2 currentPosition;
    private Vector2 previousPosition;
    private Vector3 diff;
    private float rotZ;

    //Bullet Explosion
    [SerializeField] GameObject projectileExplosion;

    [SerializeField] bool playScreenShake;


    void Start()
    {
        routeToGo = 0;
        tParam = 0f;
        StartCoroutine(GoByTheRoute(routeToGo));
    }


    void Update()
    {
     
        RotateInMoveDirection();
    }

    private IEnumerator GoByTheRoute(int routeNum)
    {
        Vector2 p0 = routes[routeNum].GetChild(0).position;
        Vector2 p1 = routes[routeNum].GetChild(1).position;
        Vector2 p2 = routes[routeNum].GetChild(2).position;
        Vector2 p3 = routes[routeNum].GetChild(3).position;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;

            objectPosition = Mathf.Pow(1 - tParam, 3) * p0 + 3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 + 3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 + Mathf.Pow(tParam, 3) * p3;
            transform.position = objectPosition;
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;

        routeToGo += 1;

        if (routeToGo > routes.Length - 1)
        {
            routeToGo = 0;
        }

        Explode();
    }

    void RotateInMoveDirection()
    {
        currentPosition = transform.position;
        Vector3 currentDirection = (currentPosition - previousPosition).normalized;
        previousPosition = transform.position;

        //Rotate in move direction
        diff = currentDirection;
        //normalize difference  
        diff.Normalize();

        //calculate rotation
        rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        //apply to object
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + 90);
    }

    void Explode()
    {
        if(projectileExplosion != null)
        {
            //Spawns in the explosion object
            GameObject explosion = Instantiate(projectileExplosion);
            explosion.transform.position = transform.position;
        }

        if(playScreenShake == true)
        {
            //call screen shake
            FindObjectOfType<screenShake>().ShakeEvent();
        }

        Destroy(gameObject);
    }
}
