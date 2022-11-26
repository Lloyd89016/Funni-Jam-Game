using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Genral")]

    [SerializeField] bool doCurve;

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

    [SerializeField] int Damage;

    [SerializeField] bool cupCake;
  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Health new_healthScript = collision.gameObject.GetComponent<Health>();
            if (new_healthScript != null)
            {
                if (Vector2.Distance(transform.position, collision.gameObject.transform.position) <= 2)
                {
                    new_healthScript.health -= Damage;
                    Explode();

                }
            }
        }
    
    }


    void Start()
    {
        if (cupCake == true)
        {
            FindObjectOfType<screenShake>().ShakeEventShoot();
        }

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
 

        //Moves the projectile
        if(doCurve == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
    }

    //Follows the bezier curve route
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
        //Spawns in the explosion prephab
        if (projectileExplosion != null)
        {
            GameObject explosion = Instantiate(projectileExplosion);
            explosion.transform.position = transform.position;
        }

        if(playScreenShake == true)
        {
            //call screen shake
            FindObjectOfType<screenShake>().ShakeEventExplode();
        }

        Destroy(gameObject);
    }
}
