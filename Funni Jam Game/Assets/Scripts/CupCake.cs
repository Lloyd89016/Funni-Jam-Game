using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupCake : MonoBehaviour
{
   
    public Transform[] routes;

    private int routeToGo;

    private bool useCurve = false;

    private float tParam;

  

    public GameObject aimPoint;

    private Vector2 objectPosition;

    public float speedModifier = 1;
    //Rotate Stuff
    private Vector2 currentPosition;
    private Vector2 previousPosition;
    private Vector3 diff;
    private float rotZ;
    [SerializeField] Rigidbody2D rb;
    //Bullet Explosion
    [SerializeField] GameObject projectileExplosion;

    [SerializeField] bool playScreenShake;


    void Start()
    {if (useCurve == true)
        {
            routeToGo = 0;
            tParam = 0f;
            StartCoroutine(GoByTheRoute(routeToGo));
        }
        Vector3 target = aimPoint.transform.position;
        target.x = target.x * 1000;
        target.y = target.y * 1000;
    }


    void Update()
    {
        float speed = speedModifier * Time.deltaTime;
        transform.Translate(aimPoint.transform.position * speed);
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



    void Explode()
    {
        if (projectileExplosion != null)
        {
            //Spawns in the explosion object
            GameObject explosion = Instantiate(projectileExplosion);
            explosion.transform.position = transform.position;
        }

        if (playScreenShake == true)
        {
            //call screen shake
            FindObjectOfType<screenShake>().ShakeEvent();
        }

        Destroy(gameObject);
    }
}
