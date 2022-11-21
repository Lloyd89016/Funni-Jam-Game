using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Genral")]

    public bool doCurve;

    [SerializeField] Transform objectSize;

    //Bullet Explosion
    [SerializeField] bool playScreenShake;
    [SerializeField] GameObject projectileExplosion;

    [Header("Curve Stuff")]

    public Transform[] routes;

    [SerializeField] LayerMask targetLayer;

    public float speedModifier;

    private int routeToGo;

    private float tParam;

    Vector2 objectPosition;

    [SerializeField] PlayerHealth playerHealth;

    [SerializeField] int damage;

    //Rotate Stuff
    private Vector2 currentPosition;
    private Vector2 previousPosition;
    private Vector3 diff;
    private float rotZ;

    [Header("Stright Shot")]

    [SerializeField] LayerMask layerMask;

    [SerializeField] float speed;

    private Vector2 target;

    public Vector3 aimPoint;

    [SerializeField] bool doShootShake;
    void Start()
    {
        routeToGo = 0;
        tParam = 0f;

        if(doCurve == true)
        {
            StartCoroutine(FollowCurve(routeToGo));
        }
        else
        {

            target = aimPoint - transform.position;
            target *= 10;
        }

        if(doShootShake == true)
        {
            FindObjectOfType<screenShake>().ShakeEventShoot();

        }
    }

    void Update()
    {
        RotateInMoveDirection();

        //If the projectile is doing a curve it only needs to detect the target, if not then it needs to detect everything
        if(doCurve == true)
        {
            //Checks to see if the projectile is touching the enemy
            Collider2D hit = Physics2D.OverlapBox(transform.position, objectSize.localScale, 0, targetLayer);

            if (hit != null)
            {
                Explode();
            }
        }
        else if(doCurve == false)
        {
            //Checks to see if the projectile is touching anything
            Collider2D hit2 = Physics2D.OverlapBox(transform.position, objectSize.localScale, 0, layerMask);

            if (hit2 != null)
            {
                Explode();
            }
        }

        //Moves the projectile
        if(doCurve == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
    }

    private IEnumerator FollowCurve(int routeNum)
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            playerHealth.TakeDamage(damage);
            Destroy(gameObject);
        }
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

        if(doCurve == true)
        {
            //call screen shake
            FindObjectOfType<screenShake>().ShakeEventExplode();
        }

        Destroy(gameObject);
    }
}
